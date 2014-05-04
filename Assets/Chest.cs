using UnityEngine;
using System.Collections;

public class Chest : Attackable {

	Vector3 initpos;
	public Transform weap_pref, statsup_pref;
	public AudioSource hit1, hit2;

	float timer = 7;

	// Use this for initialization
	public override void Start () {
		hp = 5;

		initpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer < 3){
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, timer/3f);
		}
		if(timer <= 0){
			DieWithoutDrops ();
		}

	}
	
	void FixedUpdate () {
		if(transform.position.x != initpos.x || transform.position.y != initpos.y){
			transform.position = new Vector2(Mathf.Lerp(transform.position.x, initpos.x, 0.9f), Mathf.Lerp (transform.position.y, initpos.y, 0.9f));
		}
	}

	public override void Hit(int damage){
		base.Hit (damage);
		
		AudioSource.PlayClipAtPoint(hit1.clip, transform.position);
	}

	void DieWithoutDrops(){
		base.Die ();
	}

	public override void Die(){
		Transform drop = null;

		// Drop weap, powerup or trap
		switch(Random.Range(0,1)){ // orig (0,2)
		case 0:
			// weap
			drop = weap_pref;
			break;

		case 1:
			// statsup
			drop = statsup_pref;
			break;

		case 2:
			// trap
			drop = statsup_pref;
			break;
		}
		
		Instantiate(drop, transform.position, Quaternion.identity);

		base.Die();
	}
}
