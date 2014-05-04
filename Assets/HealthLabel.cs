using UnityEngine;
using System.Collections;

public class HealthLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<GUIText>().text = "HEALTH: " + Player.hp;
	}
}
