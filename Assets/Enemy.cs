using UnityEngine;
using System.Collections;

public class Enemy : Attackable {

	public float speed;
	public bool canAtk;
	public float atkDelay;
	public float atkCounter;
	public int damage;

	public bool isCollidingWithPlayer;

	public AudioSource hit1;
	public Transform chest;
	public Transform explosion_pref;

	// Use this for initialization
	public override void Start () {
		// default stats
		hp = Random.Range(1,3);
		speed = 0.8f;

		atkCounter = 0;
		atkDelay = 0.2f;
		damage = 1;

		canAtk = false;
	}

	public override void Update(){
		base.Update();
		if(!canAtk){
			atkCounter += Time.deltaTime;

			if(atkCounter >= atkDelay){
				canAtk = true;
			}
		}
		
		UpdateAnimations();

	}

	public virtual void UpdateAnimations(){
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

	public virtual void AttemptAttackPlayer(){
		if(canAtk)
			AttackPlayer();
	}

	public virtual void AttackPlayer(){
		// do attack

		canAtk = false;
		atkCounter = 0;
	}

	public override void Hit(int damage){
		Play.Shake (Random.Range(0.03f, 0.03f));
		base.Hit (damage);

		AudioSource.PlayClipAtPoint(hit1.clip, transform.position);
	}

	public override void Die(){
		Instantiate (explosion_pref, transform.position, Quaternion.identity);
		// drop chest
		if(Random.Range (0, 5) == 0){
			Instantiate(chest, transform.position, Quaternion.identity);
		}
		base.Die ();
	}
}
