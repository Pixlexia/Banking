using UnityEngine;
using System.Collections;

public class EnemyFollowPlayer : MonoBehaviour {

	bool canMove;

	public Vector3 dir;

	// Use this for initialization
	void Start () {
		canMove = true;
	}

	void FixedUpdate () {
		if(!gameObject.GetComponent<Enemy>().isCollidingWithPlayer)
			canMove = true;

		if(canMove && !Play.gameover){
			Move ();
		}
	}

	void Move(){

		float speed = gameObject.GetComponent<Enemy>().speed;

		dir = GameObject.Find ("player").transform.position - transform.position;

		dir.Normalize();

		transform.position += dir * speed * Time.deltaTime;


//		Vector2 playerpos = GameObject.Find ("player").gameObject.transform.position;
//		
//		transform.position = Vector2.Lerp (
//			transform.position, new Vector2(playerpos.x, playerpos.y),
//			Time.deltaTime * gameObject.GetComponent<enemyscript>().speed);
	}
}
