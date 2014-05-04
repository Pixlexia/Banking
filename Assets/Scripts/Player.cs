using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static bool isFlash;
	public static float flashCounter;

	float speed = 90f;
	Vector2 move;
	public Transform attackbox_pref;
	public Transform weapon_pref;
	public Transform swish_pref;

	public Animator anim;
	public bool lookingRight;

	// STATS
	public static WeaponType weaponType;
	public static int hp;
	public static int damage;
	public static float atkDelay;
	public static float atkRadius;
	public static float knockbackForce;

	bool canAtk;
	float atkCounter;

	// sounds
	public AudioSource atk1, atk2, sethit;
	public static AudioSource hit;

	// Use this for initialization
	void Start () {
		move = new Vector2(0,0);

		hp = 50;
		damage = 1;

		atkRadius = 90f;
		atkDelay = 0.15f;
		knockbackForce = 900f;

		canAtk = true;
		atkCounter = 0;

		anim = this.GetComponent<Animator>();
		anim.speed = 1.5f;
		lookingRight = true;

		weaponType = WeaponType.sword;
		EquipWeapon(weaponType);
	}

	void Awake(){
		hit = sethit;
	}

	// Update is called once per frame
	void Update () {
//		SwitchWeapon(); // testing only

		FlashThing();

		if(!canAtk){
			atkCounter += Time.deltaTime;

			if(atkCounter >= atkDelay){
				canAtk = true;
			}
		}

		if(!Play.mainMenu && !Play.gameover && !Play.panPit)
			Movement();

//		Debug.Log (transform.position.x + " " + transform.position.y);
//		Debug.Log (Camera.main.WorldToScreenPoint(transform.position));
	}
	
	void FixedUpdate(){
		if(!Play.mainMenu && Input.GetMouseButton(0) && canAtk && !Play.gameover){
			Attack ();
		}
	}

	void SwitchWeapon(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			EquipWeapon (WeaponType.sword);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			EquipWeapon (WeaponType.axe);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			EquipWeapon (WeaponType.hammer);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			EquipWeapon (WeaponType.knife);
		}
	}

	void FlashThing(){
		if(isFlash){
			flashCounter += Time.deltaTime;
			if(flashCounter > 0.08f){
				isFlash = false;
				flashCounter = 0;
			}
		}
		
		if(isFlash){
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		else{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	void Movement3(){
		rigidbody2D.velocity = new Vector2(Mathf.Lerp(Input.GetAxis ("Horizontal") * 2, Input.GetAxis("Horizontal") * 2, 0f),
		                                   Mathf.Lerp(Input.GetAxis("Vertical") * 2, Input.GetAxis("Vertical") * 2, 0f));
	}

	void Movement2(){
		float hMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		
		float vMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		
		rigidbody2D.AddForce(new Vector2(hMove,vMove));

//		if(Input.GetKey (KeyCode.D)){
//			rigidbody2D.velocity = Vector2.right.normalized * speed * Time.deltaTime;
//		}
//		else if(Input.GetKey (KeyCode.A)){
//			rigidbody2D.velocity = -Vector2.right.normalized * speed * Time.deltaTime;
//		}
//		else{
//			rigidbody2D.velocity.x = 0;
//		}
//
//		if(Input.GetKey (KeyCode.W)){
//			rigidbody2D.velocity = Vector2.up.normalized * speed * Time.deltaTime;
//		}
//		else if(Input.GetKey (KeyCode.S)){
//			rigidbody2D.velocity = -Vector2.up.normalized * speed * Time.deltaTime;
//		}
//		else{
//			rigidbody2D.velocity.y = 0;
//		}
	}

	void Movement(){
//		Debug.Log (rigidbody2D.velocity);


		if(Input.GetKey (KeyCode.A)){
			lookingRight = true;
			move.x = -1;
			//			transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);
		}
		else if(Input.GetKey (KeyCode.D)){
			lookingRight = false;
			move.x = 1;
			//			transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);
		}
		else{
			move.x = 0;
		}
		
		if(Input.GetKey (KeyCode.W)){
			move.y = 1;
			//			transform.Translate(Vector2.up.normalized * speed * Time.deltaTime);
		}
		else if(Input.GetKey (KeyCode.S)){
			move.y = -1;
			//			transform.Translate(-Vector2.up.normalized * speed * Time.deltaTime);
		}
		else{
			move.y = 0;
		}

		anim.SetInteger("x", (int) Mathf.Abs(move.x));
		anim.SetInteger("y", (int) move.y);
		
		if(!lookingRight){
			anim.transform.rotation = Quaternion.Euler(new Vector3(anim.transform.rotation.x, 0f, anim.transform.rotation.z));
		}
		else{
			anim.transform.rotation = Quaternion.Euler(new Vector3(anim.transform.rotation.x, -180f, anim.transform.rotation.z));
		}
		
//		transform.Translate(move.normalized * speed * Time.deltaTime);
		rigidbody2D.velocity = move.normalized * speed * Time.deltaTime;
	}

	public static void EquipWeapon(WeaponType wt){
		Player.weaponType = wt;

		switch(wt){
		case WeaponType.sword:
			Weapon.swishSpeed = 700;
			atkDelay = 0.2f;
			damage = 1;
			Debug.Log ("equipped sword");
			break;
		
		case WeaponType.axe:
			atkDelay = 0.4f;
			damage = 2;
			Debug.Log ("equip axe");
			break;

		case WeaponType.hammer:
			damage = 4;
			atkDelay = 0.8f;
			Debug.Log ("hammer");
			break;

		case WeaponType.knife:
			damage = 1;
			atkDelay = 0.15f;
			Debug.Log ("knife");
			break;
		}
	}

	void Attack(){

		if(Random.Range(0,2) == 0){
			atk1.Play ();
		}
		else{
			atk2.Play ();
		}

		//rotation
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;
		float weaponangle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;

//		to scale hitbox:
//		attackbox_pref.localScale = new Vector3(10,10,0);

		Instantiate (attackbox_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
		Instantiate (weapon_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, weaponangle)));


		Instantiate(swish_pref, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

		canAtk = false;
		atkCounter = 0;
	}
	
	public static void Hit(int damage){
		if(!Play.gameover){
			hit.Play();
			
			hp -= damage;
			
			if(hp <= 0){
				Die();
			}
			
			isFlash = true;
		}
	}
	
	public static void Die(){
//		Destroy(GameObject.Find("player").gameObject);
		Play.gameover = true;

		// destroy health label
		GameObject[] objects = GameObject.FindGameObjectsWithTag("healthlabel");
		foreach(GameObject go in objects){
			go.SetActive(false);
		}

		Debug.Log ("You died");
	}

}
