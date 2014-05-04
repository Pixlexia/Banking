using UnityEngine;
using System.Collections;

public class Boss1Col : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("asd");
		// Hit by player
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockForce = Player.knockbackForce;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("boss"))
			transform.position = GameObject.Find ("boss").transform.position;
		else
			Destroy(this.gameObject);
	}
}
