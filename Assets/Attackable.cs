using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour {
	// Entities that can be attacked by player
	// Requires rigidbody2D

	public bool isFlash;
	public float flashCounter;

	public int hp;

	// Use this for initialization
	public virtual void Start () {
		hp = 1;
	}
	
	public virtual void OnTriggerEnter2D(Collider2D col){
		// Hit by player
		if(col.gameObject.tag == "attackbox"){
			Vector2 dir = transform.position - GameObject.Find("player").transform.position;
			float knockForce = Player.knockbackForce;
			rigidbody2D.AddForce(dir.normalized * knockForce);
			Hit (Player.damage);
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(isFlash){
			flashCounter += Time.deltaTime;
			if(flashCounter > 0.08f){
				isFlash = false;
				flashCounter = 0;
			}
		}
		
		if(isFlash){
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		else{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
	
	
	public virtual void Hit(int damage){
		hp -= damage;

		if(hp <= 0){
			Die();
		}

		isFlash = true;
	}
	
	public virtual void Die(){	
		Destroy (this.gameObject);
	}
}
