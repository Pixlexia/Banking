using UnityEngine;
using System.Collections;

public class WeaponPickable : Pickable {

	WeaponType wType;
	public Sprite sword, axe, hammer, knife;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		switch(Random.Range (0, 2)){
		case 0:
			wType = WeaponType.sword;
			type = "sword";
			GetComponent<SpriteRenderer>().sprite = sword;
			break;
		
		case 1 :
			wType = WeaponType.axe;
			type = "axe";
			GetComponent<SpriteRenderer>().sprite = axe;
//			transform.Rotate(new Vector3(0,0,-45f));
			break;

		case 2:
			// hammer;
			wType = WeaponType.hammer;
			type = "hammer";
			GetComponent<SpriteRenderer>().sprite = hammer;
			break;

		case 3:
			// knife;
			wType = WeaponType.knife;
			type = "knife";
			GetComponent<SpriteRenderer>().sprite = knife;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Picked(){
		base.Picked();
		Player.EquipWeapon(wType);
	}
}
