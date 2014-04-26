using UnityEngine;
using System.Collections;

public class pillarscript : MonoBehaviour {

	public static int pillarHP = 1000;
	public AudioSource hit1, hit2;

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.position.x != 0 || transform.position.y != 0){
			transform.position = new Vector2(Mathf.Lerp(transform.position.x, 0, 0.9f), Mathf.Lerp (transform.position.y, 0, 0.9f));
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockbackForce = 400f;
			rigidbody2D.AddForce(dir.normalized * knockbackForce);
			Hit (Player.damage);
		}
	}

	void Hit(int damage){
		pillarHP -= damage;

		if(pillarHP <= 0){
			Die();
		}
	}

	void Die(){
		Debug.Log ("go to boss");
	}

	void OnGUI(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
//		Debug.Log (pos.x + " " + pos.y);
//		Debug.Log ("pillar " + Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(pos.x - 20, Screen.height - pos.y - 60, 100, 20), "HP: " + pillarHP);
	}
}
