using UnityEngine;
using System.Collections;

public class enemy_followplayer : MonoBehaviour {

	bool canMove;

	// Use this for initialization
	void Start () {
		canMove = true;
	}

	void FixedUpdate () {
		if(!gameObject.GetComponent<enemyscript>().isCollidingWithPlayer)
			canMove = true;

		if(canMove){
			Move ();
		}
	}

	void Move(){

		float speed = gameObject.GetComponent<enemyscript>().speed;

		Vector3 dir = GameObject.Find ("player").transform.position - transform.position;

		dir.Normalize();

		transform.position += dir * speed * Time.deltaTime;


//		Vector2 playerpos = GameObject.Find ("player").gameObject.transform.position;
//		
//		transform.position = Vector2.Lerp (
//			transform.position, new Vector2(playerpos.x, playerpos.y),
//			Time.deltaTime * gameObject.GetComponent<enemyscript>().speed);
	}
}
