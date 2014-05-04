using UnityEngine;
using System.Collections;

public class BossBounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameObject.Find("boss").transform.position;
	}

	void OnTriggerEnter2D(Collider2D col){
		// Hit by player
		Debug.Log (col.gameObject.name);
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockForce = Player.knockbackForce;
			rigidbody2D.AddForce(dir.normalized * knockForce);
			GameObject.Find ("boss").GetComponent<Boss>().Hit (Player.damage);
		}
	}
}
