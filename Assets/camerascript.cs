using UnityEngine;
using System.Collections;

public class camerascript : MonoBehaviour {

	float smooth = 6.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 playerpos = GameObject.Find ("player").gameObject.transform.position;
		Vector2 offset;
		
		offset.x = Input.mousePosition.x - 480;
		offset.y = Input.mousePosition.y - 300;
//		offset.x = 0;
//		offset.y = 0;

		float mod = 2000;

		transform.position = Vector3.Lerp (
			transform.position, new Vector3(playerpos.x + offset.x/mod, playerpos.y + offset.y/mod, transform.position.z),
			Time.deltaTime * smooth);

	}
}
