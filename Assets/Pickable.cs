using UnityEngine;
using System.Collections;

public class Pickable : MonoBehaviour {

	string type;

	// Use this for initialization
	void Start () {
		switch(Random.Range (0,2)){
		case 0:
			type = "1";
			break;

		case 1:
			type = "2";
			break;

		default:
			type = "default";
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.name == "player"){
			Picked();
		}
	}

	void Picked(){
		Debug.Log ("" + type);

		Destroy(this.gameObject);
	}
}
