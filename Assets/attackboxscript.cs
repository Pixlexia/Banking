using UnityEngine;
using System.Collections;

public class attackboxscript : MonoBehaviour {

	float counter;

	// Use this for initialization
	void Start () {
		counter = 0;
		float x = 0, y = 0;

		switch(Player.weaponType){
		case WeaponType.sword:
			x = 1f;
			y = 1f;
			break;
			
		case WeaponType.axe:
			x = 1.25f;
			y = 1.25f;
			break;
			
		case WeaponType.hammer:
			x = 1.75f;
			y = 1.25f;
			break;
			
		case WeaponType.knife:
			x = 0.25f;
			y = 0.25f;
			break;
		}

		Debug.Log (x + " " + y);
//		transform.localScale = new Vector3(x, y, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if(counter > 0.02f)
			Destroy(this.gameObject);
	}
}
