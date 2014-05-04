using UnityEngine;
using System.Collections;

public class Statsup : Pickable {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		type = "im a statsup";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Picked(){
		base.Picked ();
		Player.hp += 5;
	}
}
