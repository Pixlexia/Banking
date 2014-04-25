using UnityEngine;
using System.Collections;

public class camerascript : MonoBehaviour {

	float smooth = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerpos = GameObject.Find ("player").gameObject.transform.position;
		Vector2 offset;

//		offset.x = Input.mousePosition.x - 480;
//		offset.y = Input.mousePosition.y - 300;

		transform.position = Vector3.Lerp (
			transform.position, new Vector3(playerpos.x, playerpos.y, transform.position.z),
			Time.deltaTime * smooth);

		Debug.Log (transform.position.x + " " + transform.position.y);
	}
}
