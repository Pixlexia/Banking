using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.name == "player" && Pitcover.ok){
			Application.LoadLevel("boss");
		}
	}
}