using UnityEngine;
using System.Collections;

public class enemyscript : MonoBehaviour {

	public float speed;
	public bool canAtk;
	public float atkDelay;
	public float atkCounter;
	public int damage;
	public int hp;

	public bool isCollidingWithPlayer;

	// Use this for initialization
	void Start () {
		// default stats
		hp = Random.Range(1,3);
		speed = 0.8f;

		atkCounter = 0;
		atkDelay = 0.2f;
		damage = 1;
	}

	void Update(){
		if(!canAtk){
			atkCounter += Time.deltaTime;

			if(atkCounter >= atkDelay){
				canAtk = true;
			}
		}

		if(isCollidingWithPlayer){
			AttemptAttackPlayer();
		}
	}

	void FixedUpdate () {
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.name == "player")
			isCollidingWithPlayer = true;
	}
	
	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.name == "player")
			isCollidingWithPlayer = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "attackbox"){
			Debug.Log ("hit");
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockForce = Player.knockbackForce;
			rigidbody2D.AddForce(dir.normalized * knockForce);
			Hit (Player.damage);
		}
	}

	void AttemptAttackPlayer(){
		if(canAtk)
			AttackPlayer();
	}

	void AttackPlayer(){
		Player.Hit(damage);

		canAtk = false;
		atkCounter = 0;
	}

	void Hit(int damage){
		hp -= damage;

		if(hp <= 0){
			Die();
		}

		GetComponent<SpriteRenderer>().color = Color.red;
	}

	void Die(){
		Destroy (this.gameObject);
	}
}
