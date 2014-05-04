using UnityEngine;
using System.Collections;

public class Blockstairs : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Play.levelDone){
			Destroy (this.gameObject);
		}
	}
}
