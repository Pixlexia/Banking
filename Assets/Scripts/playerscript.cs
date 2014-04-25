using UnityEngine;
using System.Collections;

public class playerscript : MonoBehaviour {

	float speed = 2f;
	Vector2 move;
	public Transform attackbox_pref;

	// Use this for initialization
	void Start () {
		move = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey (KeyCode.A)){
			move.x = -1;
//			transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);
		}
		else if(Input.GetKey (KeyCode.D)){
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

		if(Input.GetMouseButtonDown(0)){
			Attack ();
		}

		transform.Translate(move.normalized * speed * Time.deltaTime);

	}

	void Attack(){
		Instantiate(attackbox_pref, transform.position, Quaternion.identity);
	}

	void FixedUpdate(){
		// rigidbody2D.velocity = new Vector2(Mathf.Lerp(Input.GetAxis ("Horizontal") * 2, Input.GetAxis("Horizontal") * 2, 0f),
		   //                              Mathf.Lerp(Input.GetAxis("Vertical") * 2, Input.GetAxis("Vertical") * 2, 0f));

	}

}
