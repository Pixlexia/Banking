using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	public Transform chicken_pref;
	public Transform eyeball_pref;
	public Transform swish_pref;
	public Texture2D cursorImage;
	public Transform chest_pref;

	float counter, nextTimer;

	public bool summonEnemies;
	public static bool mainMenu;
	public static bool levelDone;
	public static bool gameover;
	public static bool win;

	public static float shake = 0;
	float shakeAmount = 0.06f;
	float decreaseFactor = 0.8f;
	public static bool shaking;
	public static Vector3 camAftershake;

	public static bool panPit;
	float panpitTimer;

	// Use this for initialization
	void Start () {
		float s_baseOrthographicSize = Screen.height / 150.0f / 2.0f;
		Camera.main.orthographicSize = s_baseOrthographicSize;

		Camera.main.transform.position = new Vector3(0,0,-10);

		Screen.showCursor = false;

		levelDone = false;
		counter = 0;

		if(Application.loadedLevelName == "surface"){
			summonEnemies = true;
			GameObject.Find ("player").GetComponent<Animator>().enabled = true;
			GameObject.Find ("player").GetComponent<SpriteRenderer>().enabled = true;

		}
		else{
			summonEnemies = false;
		}



		panPit = false;
		shaking = false;
		mainMenu = true;
		gameover = false;
		panpitTimer = 0;
		gameover = false;
	}

	public static void Shake(float amount){
		camAftershake = Camera.main.transform.position;
		shake = amount;
		shaking = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameover){
			summonEnemies = false;
		
			// destroy player
			Destroy (GameObject.Find ("player"));

			// show gameover labels
			GameObject[] objects = GameObject.FindGameObjectsWithTag("gameoverlabel");
			foreach(GameObject go in objects){
				go.GetComponent<GUIText>().enabled = true;
			}
			
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(Application.loadedLevelName);
			}
		}

		if(GameObject.Find ("title")){
			if(Input.GetMouseButtonDown(0)){
				if(Application.loadedLevelName == "mainmenu"){
					Application.LoadLevel ("surface");
				}
			}
		}
		else{
			mainMenu = false;
			float x;
			if(Application.loadedLevelName == "surface")
				x = 300f;
			else
				x = 200f;

			float s_baseOrthographicSize = Screen.height / x / 2.0f;

			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, s_baseOrthographicSize, Time.deltaTime * 5f);
		}

//		if(Input.GetKeyDown (KeyCode.RightArrow)){
//			Application.LoadLevel("boss");
//		}
//		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
//			Application.LoadLevel("surface");
//		}

		if(shaking){
			if(shake > 0){
				Camera.main.transform.position += Random.insideUnitSphere * shakeAmount;
				shake -= Time.deltaTime * decreaseFactor;
			}
			else{ // shake ends
				shake = 0.0f;
				shaking = false;
			}
		}

		// all pillars destroyed
		if(!levelDone && GameObject.FindGameObjectsWithTag("pillar").Length <= 0 && Application.loadedLevelName == "surface"){
			summonEnemies = false;
			levelDone = true;
			counter = 0;
			panPit = true;

			// kill all enemies
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
			foreach(GameObject go in enemies){
				go.SetActive(false);
			}
		}

		if(panPit){
			panpitTimer += Time.deltaTime;

			if(panpitTimer > 10f){
				panPit = false;
			}
		}

		if(Application.loadedLevelName == "surface" && levelDone && counter < 7f){
			Shake (1f);
			counter += Time.deltaTime;
		}

		// spawn
		if(counter > nextTimer && summonEnemies){
			int i;
			if(GameObject.FindObjectsOfType (typeof(Eyeball)).Length > 6){
				i = 1;
			}
			else{
				i = Random.Range(0,3);
			}
			if(i == 0){
				Instantiate (eyeball_pref, new Vector3(Random.Range (-1, 2) * 4, Random.Range (-1,2) * 2.7f, 0), Quaternion.identity);
			}
			else{
				Instantiate (chicken_pref, new Vector3(Random.Range (-1, 2) * 4, Random.Range (-1,2) * 2.7f, 0), Quaternion.identity);
			}
			counter = 0;
			nextTimer = Random.Range (0.1f, 2f);
		}
		else if(summonEnemies){
			counter += Time.deltaTime;
		}
	}
	
	void OnGUI(){
		//GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorImage.width/2, Screen.height - Input.mousePosition.y - cursorImage.height/2, cursorImage.width, cursorImage.height), cursorImage);
	}
}
