using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	public Transform enemy_pref;
	public Texture2D cursorImage;

	float counter;

	// Use this for initialization
	void Start () {
		float s_baseOrthographicSize = Screen.height / 300.0f / 2.0f;
		Camera.main.orthographicSize = s_baseOrthographicSize;

		Screen.showCursor = false;

		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(counter > 1f){
			Instantiate (enemy_pref, new Vector3(Random.Range(-3,3), Random.Range (-3,3), 0), Quaternion.identity);
			counter = 0;
		}
		else{
			counter += Time.deltaTime;
		}
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorImage.width/2, Screen.height - Input.mousePosition.y - cursorImage.height/2, cursorImage.width, cursorImage.height), cursorImage);
		GUI.Label(new Rect(10,10,100,30), "Health: " + Player.hp);
	}
}
