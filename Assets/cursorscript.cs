using UnityEngine;
using System.Collections;

public class cursorscript : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D col){
	}

	void OnTriggerExit2D(Collider2D col){
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
		transform.position = pos;
	}
}
