using UnityEngine;
using System.Collections;

public class enemyscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.tag == "attackbox"){
			Debug.Log ("hit");
			Hit ();
		}
	}

	void Hit(){
		Die();
	}

	void Die(){
		Destroy (this.gameObject);
	}
}
