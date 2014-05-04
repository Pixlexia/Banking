using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	public int damage;
	public Vector3 startPos, targetPos;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("boss")){
			damage = 1;
			speed = Boss.bulletSpeed;
		}
		else{
			speed = Eyeball.bulletSpeed;
			damage = 1;
		}


		// Vector3 target = GameObject.Find ("player").transform.position;
		//
		//Vector3 objectPos = transform.position;
		//target.x = target.x - objectPos.x;
		//target.y = target.y - objectPos.y;
		//
		//float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
		//transform.Rotate(new Vector3(0, 0, angle));

		// Rotate the bullet first when instantiating
		rigidbody2D.AddForce(transform.up * speed);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name == "player"){
			Player.Hit(damage);
			Destroy(this.gameObject);
		}

		if(col.gameObject.name == "borders"){
			
			Destroy(this.gameObject);
		}
	}
}
