using UnityEngine;
using System.Collections;

public class play : MonoBehaviour {

	public Transform enemy_pref;

	// Use this for initialization
	void Start () {
		float s_baseOrthographicSize = Screen.height / 192.0f / 2.0f;
		Camera.main.orthographicSize = s_baseOrthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
