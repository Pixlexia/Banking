using UnityEngine;
using System.Collections;

public class camerascript : MonoBehaviour {

	float smooth = 7.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float targetX = 0, targetY = 0;
		if(Play.mainMenu){
			// main menu
			Vector2 playerpos = new Vector2(0,0);
			
			Vector2 offset;
			
			offset.x = Input.mousePosition.x - 480;
			offset.y = Input.mousePosition.y - 300;
			
			float mod = 1600;
			
			targetX = playerpos.x + offset.x/mod;
			targetY = playerpos.y + offset.y/mod;	
			
			smooth = 7f;
		}
		else if(!Play.mainMenu){
			// pan to pit after 4 pillars destroyed
			if(Play.panPit && Application.loadedLevelName == "surface"){
				targetX = 0;
				targetY = 0;

				smooth = 0.5f;
			}
			else {
				Vector2 playerpos;
				if(!Play.gameover){ // default
					playerpos = GameObject.Find ("player").gameObject.transform.position;
					smooth = 7f;
				}
				else{ // gameover screen
					playerpos = new Vector2(0,0);
					smooth = 0.5f;
				}

				Vector2 offset;
				
				offset.x = Input.mousePosition.x - 480;
				offset.y = Input.mousePosition.y - 300;
				//		offset.x = 0;
				//		offset.y = 0;
				
				float mod = 1600;
				
				targetX = playerpos.x + offset.x/mod;
				targetY = playerpos.y + offset.y/mod;
			}

		}
		transform.position = Vector3.Lerp (
			transform.position, new Vector3(targetX, targetY, transform.position.z),
			Time.deltaTime * smooth);
	}
}
