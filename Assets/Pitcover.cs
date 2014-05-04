using UnityEngine;
using System.Collections;

public class Pitcover : MonoBehaviour {

	float start;

	public static bool ok;
	float timer;

	// Use this for initialization
	void Start () {
		ok = false;
	}
	
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);

		if(Play.levelDone){
			timer += Time.deltaTime;

			if(timer >= 2f){
				transform.position = Vector3.Lerp (
					transform.position, new Vector3(transform.position.x, 0.8f, transform.position.z),
					Time.deltaTime * 0.3f);
			}
		}

		if(transform.position.y >= 0.7f){
			ok = true;
		}

		 
	}
}
