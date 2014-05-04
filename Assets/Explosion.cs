using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Animator>().speed = 1f;
	}
	
	private IEnumerator KillOnAnimationEnd() {
		yield return new WaitForSeconds (0.2f);
		Destroy (this.gameObject);
	}
	
	void Update () {
		
		StartCoroutine (KillOnAnimationEnd ());
	}
}
