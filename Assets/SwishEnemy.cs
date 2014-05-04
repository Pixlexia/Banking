using UnityEngine;
using System.Collections;

public class SwishEnemy : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Vector3 target = new Vector3(0,0,0);

		if(!Play.gameover){
			target = GameObject.Find ("player").transform.position;		
		}

		target.z = 5.23f;
		
		Vector3 objectPos = transform.position;
		target.x = target.x - objectPos.x;
		target.y = target.y - objectPos.y;
		
		float angle2 = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg + 60f;
		
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