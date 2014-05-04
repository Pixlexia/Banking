using UnityEngine;
using System.Collections;

public class Chicken : Enemy {
	public Transform swish_pref;
	public Animator anim;
	bool lookingRight;

	// Use this for initialization
	void Start () {
		base.Start();
		anim = this.GetComponent<Animator>();
		speed = 0.9f;
		hp = 2;
		atkDelay = 0.9f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();

		if(isCollidingWithPlayer){
			AttemptAttackPlayer();
		}
	}

	public override void AttackPlayer(){
		base.AttackPlayer ();
		Player.Hit (damage);
		Instantiate(swish_pref, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
	}

	public override void UpdateAnimations(){
		base.UpdateAnimations();
		float x = this.gameObject.GetComponent<EnemyFollowPlayer>().dir.x * 10f;
		float y = this.gameObject.GetComponent<EnemyFollowPlayer>().dir.y * 10f;
		
		anim.SetInteger("x", (int) Mathf.Abs(x));
		anim.SetInteger("y", (int) y);

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
