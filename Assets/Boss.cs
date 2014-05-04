using UnityEngine;
using System.Collections;

public class Boss : Enemy {

	public Transform bullet_pref;
	public static int bulletSpeed;

	float behaviorTimer;

	public int form;
	public int behaviorState;

	public Animator anim;
	public Animator form1, form2;

	float chargeTimer;
	Vector3 chargeTarget;
	bool charging, gotoMid;
	int chargeCount;
	int maxHP = 140;

	bool isDef;
	float defCounter;

	// death
	float deadCounter, explodeCounter;

	public AudioSource metal;

	// Use this for initialization
	public override void Start () {
		base.Start();
		hp = maxHP;

		atkDelay = 0.4f;
		behaviorState = -2; // orig -2
		form = 1;

		anim = this.GetComponent<Animator>();
		charging = false;
		chargeTimer = 0;
		gotoMid = false;
		chargeCount = 0;
		bulletSpeed = 50;

		deadCounter = 0;
		explodeCounter = 0;
	}

	public override void Hit (int damage)
	{
		if(form == 2 || (form == 1 && (behaviorState == 3 || behaviorState == 0))){
			base.Hit(damage);
			if(form == 1 && hp <= maxHP/2){
				behaviorState = 4;
				behaviorTimer = 0;
			}
		}
		else{
			metal.Play();
			isDef = true;
		}
		Debug.Log (hp);
	}

	public override void Die(){
		
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(hp > 0){
			anim.SetInteger("BehaviorState", behaviorState);
			anim.SetInteger ("Form", form);
			behaviorTimer += Time.deltaTime;
			
			// STATE MACHINE
			if(form == 1){
				switch(behaviorState){
				case -2:
					// during start, wait for 3 secs to prepare
					if(behaviorTimer > 3){
						behaviorState = 1;
						behaviorTimer = 0;
					}
					break;
					
				case 0:	// rest
					canAtk = false;
					
					if(behaviorTimer > 5){
						behaviorState = 1;
						behaviorTimer = 0;
					}
					break;
					
				case 1:// fire to player
					atkDelay = 0.3f;
					bulletSpeed = 90;
					
					if(behaviorTimer > 5){
						canAtk = false;
					}
					if(behaviorTimer > 6){
						behaviorState = 2;
						behaviorTimer = 0;
					}
					break;
					
				case 2:  // bullet hell flurry
					atkDelay = 0.2f;
					bulletSpeed = 50;
					
					if(behaviorTimer > 6){
						canAtk = false;
					}
					if(behaviorTimer > 7){
						behaviorState = 3;
						behaviorTimer = 0;
					}
					break;
					
				case 3: // drop rocks
					atkDelay = 0.3f;
					
					if(behaviorTimer > 4){
						behaviorState = 0;
						behaviorTimer = 0;
					}
					break;
					
				case 4: // transform to final form
					Play.Shake (4f);
					if(behaviorTimer > 3){
						form = 2;
						behaviorState = -2;
						behaviorTimer = 0;
						Destroy(GameObject.Find ("boss1col").gameObject);
						gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
					}
					if(behaviorTimer > 5){
						
					}
					break;
				}
			}
			else if(form == 2){
				switch(behaviorState){
				case -2: // rest
					if(behaviorTimer > 3){
						behaviorState = 1;
						behaviorTimer = 0;
						chargeCount = 0;
					}
					break;
					
				case 1: // charge
					atkDelay = 1f;
					gameObject.GetComponent<Animator>().speed = 3f;
					if(behaviorTimer > 13){
						behaviorState = 2;
						behaviorTimer = 0;
						gotoMid = true;
						charging = false;
					}
					break;
					
				case 2: // go to center
					canAtk = false;
					gameObject.GetComponent<Animator>().speed = 1f;
					if(behaviorTimer > 3){
						behaviorState = 3;
						behaviorTimer = 0;
						gotoMid = false;
						canAtk = true;
					}
					break;
					
				case 3: // holyshit bullethell
					atkDelay = 0.3f;
					bulletSpeed = 50;
					if(behaviorTimer > 4){
						behaviorState = 4;
						behaviorTimer = 0;
					}
					break;
					
				case 4: // fire 360
					atkDelay = 0.2f;
					bulletSpeed = 60;
					if(behaviorTimer > 5){
						behaviorState = 5;
						behaviorTimer = 0;
					}
					break;
					
				case 5: // fire opposite
					if(behaviorTimer > 5){
						behaviorState = -2;
						behaviorTimer = 0;
					}
					break;
					
				}
			}
			
			if(Play.gameover)
				canAtk = false;
			
			if(canAtk){
				AttackPlayer();
			}
			
			if(charging){
				chargeTimer += Time.deltaTime;
				if(chargeTimer > 0.05f){
					ChargeMove();
				}
			}
			
			if(gotoMid){
				GotoMid();
			}
			
			if(isDef){
				defCounter += Time.deltaTime;
				if(defCounter > 0.08f){
					isDef = false;
					defCounter = 0;
				}
			}
			
			if(isDef){
				GetComponent<SpriteRenderer>().color = Color.gray;
			}
			else if(!isFlash){
				GetComponent<SpriteRenderer>().color = Color.white;
			}

		} // exploding death
		else if(hp <= 0 && deadCounter < 4f){
			Debug.Log ("dead");
			DeathExplosions();
		} // dead
		else{
			// display gameover text
			GameObject[] go = GameObject.FindGameObjectsWithTag("wintext");
			foreach(GameObject g in go){
				g.gameObject.GetComponent<GUIText>().enabled = true;
			}
			Play.win = true;
			Destroy(this.gameObject);
		}

	}

	void DeathExplosions(){
		Play.Shake (2f);
		explodeCounter += Time.deltaTime;
		deadCounter += Time.deltaTime;
		if(explodeCounter > Random.Range (0f,0.03f)){
			Instantiate(explosion_pref, new Vector3(transform.position.x + Random.Range (-2f,2f), transform.position.y + Random.Range (-2f,2f), transform.position.z), Quaternion.identity);
			explodeCounter = 0;
		}
	}

	
	public override void AttackPlayer(){
		if(form == 1){
			switch(behaviorState){
			case 1:
				FireToPlayer();
				break;
				
			case 2:
				FireBulletHell360();
				break;
				
			case 3:
				DropRocks ();
				break;
				
			default:
				break;
			}
		}
		else{ // final form
			switch(behaviorState){
			case 1:
				if(chargeCount <= 10) // max charges = 10
					Charge();
				break;
				
			case 2:
				break;
				
			case 3: // fire bullethell
				FireBulletHell360();
				FireBulletHell360_2();
				break;

			case 4:
				FireBulletHell360_2();
				break;

			case 5:
				FireBulletHell360();
				break;
			}
		}
		
		canAtk = false;
		atkCounter = 0;
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
		// Hit by player
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockForce = Player.knockbackForce;
			rigidbody2D.AddForce(dir.normalized * knockForce);
			Hit (Player.damage);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.name == "player"){
			Player.Hit (4);
		}
	}

	void GotoMid(){

		transform.position = Vector3.Lerp (
			transform.position, new Vector3(0,0,0),
			Time.deltaTime * 5f);
	}

	void ChargeMove(){
		transform.position = Vector3.Lerp (
			transform.position, chargeTarget,
			Time.deltaTime * 5f);

		if(transform.position == chargeTarget){
			charging = false;
			chargeTimer = 0;
		}
	}

	// call charge on attack every 2s
	void Charge(){
		chargeTimer = 0;
		chargeTarget = (GameObject.Find ("player").transform.position) * 1.1f;
		charging = true;
		chargeCount++;
	}

	void DropRocks(){
		// drop rock
		FireBulletHell360();
	}

	void FireToPlayer(){
		Vector3 origTarget = GameObject.Find ("player").transform.position;
		Vector3 startPos, target;
		float angle;

		startPos = transform.position;
		
		// left arm
		startPos.x = transform.position.x - 0.76f;
		startPos.y = transform.position.y + 0.12f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
		
		startPos.x = transform.position.x - 0.76f;
		startPos.y = transform.position.y + 0.01f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
		
		startPos.x = transform.position.x - 0.91f;
		startPos.y = transform.position.y + 0.01f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));


		// right arm
		startPos.x = transform.position.x + 0.76f;
		startPos.y = transform.position.y + 0.12f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
		
		startPos.x = transform.position.x + 0.76f;
		startPos.y = transform.position.y + 0.01f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
		
		startPos.x = transform.position.x + 0.91f;
		startPos.y = transform.position.y + 0.01f;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
	}
	
	void FireBulletHell360(){
		int numberOfBullets = 6;
		for(int i = 0 ; i < numberOfBullets; i++){
			Instantiate(bullet_pref, new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z), Quaternion.Euler(new Vector3(0,0, (float) i * (360f/numberOfBullets) + (behaviorTimer*2*1000/36))));
		}
	}
	
	void FireBulletHell360_2(){
		int numberOfBullets = 6;
		for(int i = 0 ; i < numberOfBullets; i++){
			Instantiate(bullet_pref, new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z), Quaternion.Euler(new Vector3(0,0, (float) i * (360f/numberOfBullets) - (behaviorTimer*2*1000/36))));
		}
	}

	void OnGUI(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
	}
}
