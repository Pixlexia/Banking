
using UnityEngine;
using System.Collections;

public class Eyeball : Enemy {

	public Transform bullet_pref;
	public static float bulletSpeed;

	public Animator anim;

	// Use this for initialization
	void Start () {
		base.Start ();

		speed = 0.7f;
		bulletSpeed = 60f;
		atkDelay = 0.5f;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();

		if(!Play.gameover)
			Move();
	}

	void Move(){
		float distance = Vector3.Distance(transform.position, GameObject.Find ("player").transform.position);
		if(distance > 0.7f){
			MoveToPlayer();
		}
		else {
			AttemptAttackPlayer();
		}
	}

	public override void AttackPlayer(){
		base.AttackPlayer();

		Vector3 origTarget = GameObject.Find ("player").transform.position;
		Vector3 startPos, target;
		float angle;
		
		startPos = transform.position;
		
		// fire projetile
		startPos.x = transform.position.x;
		startPos.y = transform.position.y;
		target.x = origTarget.x - startPos.x;
		target.y = origTarget.y - startPos.y;
		angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		Instantiate(bullet_pref, startPos, Quaternion.Euler(new Vector3(0,0, angle)));
	}

	void MoveToPlayer(){
		float speed = gameObject.GetComponent<Enemy>().speed;
		
		Vector3 dir = GameObject.Find ("player").transform.position - transform.position;
		
		dir.Normalize();
		
		transform.position += dir * speed * Time.deltaTime;
		
		float x = dir.x * 10f;
		float y = dir.y * 10f;
		
		anim.SetInteger("x", (int) Mathf.Abs(x));
		anim.SetInteger("y", (int) y);

		bool lookingRight;
		
		if(x < 0){
			lookingRight = true;
		}
		else{
			lookingRight = false;
		}
		
		if(!lookingRight){
			anim.transform.rotation = Quaternion.Euler(new Vector3(anim.transform.rotation.x, 0f, anim.transform.rotation.z));
		}
		else{
			anim.transform.rotation = Quaternion.Euler(new Vector3(anim.transform.rotation.x, -180f, anim.transform.rotation.z));
		}
	}
}
