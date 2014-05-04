using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	float target;

	float counter = 90;
	public static float swishSpeed = 900;

	public Sprite sp_sword, sp_axe, sp_hammer, sp_knife;
	public Sprite thisSprite;

	// Use this for initialization
	void Start () {
		target = transform.rotation.eulerAngles.z - Player.atkRadius;

		transform.localScale = new Vector3(1,1,0);

		switch(Player.weaponType){
		case WeaponType.sword:
			thisSprite = sp_sword;
			transform.localScale = new Vector3(0.75f, 0.75f, 0);
			break;

		case WeaponType.axe:
			thisSprite = sp_axe;
			break;

		case WeaponType.hammer:
			thisSprite = sp_hammer;
			break;

		case WeaponType.knife:
			thisSprite = sp_knife;
			break;
		}

		GetComponent<SpriteRenderer>().sprite = thisSprite;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!Play.gameover)
			transform.position = GameObject.Find("player").transform.position;

		// transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(transform.rotation.eulerAngles.z, target, Time.deltaTime * 20f)));

		// Debug.Log (transform.rotation.eulerAngles.z + " AND " + target);

		transform.Rotate(0,0,swishSpeed * Time.deltaTime);

		counter -= swishSpeed * Time.deltaTime;

		//if(Mathf.Round (transform.rotation.eulerAngles.z) == Mathf.Round (target)){
		if(counter <= 0){
			Destroy(this.gameObject);
		}
	}
}
