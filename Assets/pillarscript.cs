using UnityEngine;
using System.Collections;

public class pillarscript : Attackable {

	public AudioSource hit1, hit2;

	Vector3 initpos;
	public Transform explosion_pref, fallenpillar_pref;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		hp = 50;

		initpos = transform.position;
	}

	public override void Update(){
		base.Update();
	}

	void FixedUpdate () {
		if(transform.position.x != 0 || transform.position.y != 0){
			transform.position = new Vector2(Mathf.Lerp(transform.position.x, initpos.x, 0.9f), Mathf.Lerp (transform.position.y, initpos.y, 0.9f));
		}
	}
	/*
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockbackForce = 400f;
			rigidbody2D.AddForce(dir.normalized * knockbackForce);
			Hit (Player.damage);
		}
	}
	*/

	public override void Hit(int damage){
		base.Hit (damage);
		Play.Shake (Random.Range (0.0f, 0.02f) + 0.03f);

		if(Random.Range(0,2) == 0){
			AudioSource.PlayClipAtPoint(hit1.clip, transform.position);
		}
		else{
			AudioSource.PlayClipAtPoint(hit2.clip, transform.position);
		}
	}

	public override void Die(){
		Play.Shake(2f);
		Instantiate (explosion_pref, transform.position, Quaternion.identity);
		Instantiate (fallenpillar_pref, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	void OnGUI(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
	}
}
