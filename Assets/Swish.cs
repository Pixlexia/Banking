using UnityEngine;
using System.Collections;

public class Swish : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle2 = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 60f;

		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle2));
	}

	private IEnumerator KillOnAnimationEnd() {
		yield return new WaitForSeconds (0.16f);
		Destroy (this.gameObject);
	}
	
	void Update () {
		if(!Play.gameover)
			transform.position = GameObject.Find("player").transform.position;

		StartCoroutine (KillOnAnimationEnd ());
	}
}
