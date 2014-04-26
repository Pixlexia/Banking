using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float speed = 90f;
	Vector2 move;
	public Transform attackbox_pref;

	public Animator anim;

	// STATS
	public static int hp;
	public static int damage;
	public static float atkDelay;
	public static int atkRadius;
	public static float knockbackForce;

	bool canAtk;
	float atkCounter;

	// sounds
	public AudioSource atk1, atk2;

	// Use this for initialization
	void Start () {
		move = new Vector2(0,0);

		hp = 10;
		damage = 1;
		atkDelay = 0.2f;
		knockbackForce = 500f;

		canAtk = true;
		atkCounter = 0;

		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!canAtk){
			atkCounter += Time.deltaTime;

			if(atkCounter >= atkDelay){
				canAtk = true;
			}
		}
		Movement();

//		Debug.Log (transform.position.x + " " + transform.position.y);
//		Debug.Log (Camera.main.WorldToScreenPoint(transform.position));
	}
	
	void FixedUpdate(){
		if(Input.GetMouseButton(0) && canAtk){
			Attack ();
		}
	}

	void Movement3(){
		rigidbody2D.velocity = new Vector2(Mathf.Lerp(Input.GetAxis ("Horizontal") * 2, Input.GetAxis("Horizontal") * 2, 0f),
		                                   Mathf.Lerp(Input.GetAxis("Vertical") * 2, Input.GetAxis("Vertical") * 2, 0f));
	}

	void Movement2(){
		float hMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		
		float vMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		
		rigidbody2D.AddForce(new Vector2(hMove,vMove));

//		if(Input.GetKey (KeyCode.D)){
//			rigidbody2D.velocity = Vector2.right.normalized * speed * Time.deltaTime;
//		}
//		else if(Input.GetKey (KeyCode.A)){
//			rigidbody2D.velocity = -Vector2.right.normalized * speed * Time.deltaTime;
//		}
//		else{
//			rigidbody2D.velocity.x = 0;
//		}
//
//		if(Input.GetKey (KeyCode.W)){
//			rigidbody2D.velocity = Vector2.up.normalized * speed * Time.deltaTime;
//		}
//		else if(Input.GetKey (KeyCode.S)){
//			rigidbody2D.velocity = -Vector2.up.normalized * speed * Time.deltaTime;
//		}
//		else{
//			rigidbody2D.velocity.y = 0;
//		}
	}

	void Movement(){
		if(Input.GetKey (KeyCode.A)){
			move.x = -1;
			//			transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);
		}
		else if(Input.GetKey (KeyCode.D)){
			move.x = 1;
			//			transform.Translate(Vector2.right.normalized * speed * Time.deltaTime);
		}
		else{
			move.x = 0;
		}
		
		if(Input.GetKey (KeyCode.W)){
			move.y = 1;
			//			transform.Translate(Vector2.up.normalized * speed * Time.deltaTime);
		}
		else if(Input.GetKey (KeyCode.S)){
			move.y = -1;
			//			transform.Translate(-Vector2.up.normalized * speed * Time.deltaTime);
		}
		else{
			move.y = 0;
		}
		
//		transform.Translate(move.normalized * speed * Time.deltaTime);
		rigidbody2D.velocity = move.normalized * speed * Time.deltaTime;
	}

	void Attack(){
		if(Random.Range(0,2) == 0){
			atk1.Play ();
		}
		else{
			atk2.Play ();
		}

		//rotation
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;

//		to scale hitbox:
//		attackbox_pref.localScale = new Vector3(10,10,0);

		Instantiate(attackbox_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));

		canAtk = false;
		atkCounter = 0;
	}
	
	public static void Hit(int damage){
		hp -= damage;
		
		if(hp <= 0){
			Die();
		}
	}
	
	public static void Die(){
//		Destroy(GameObject.Find("player").gameObject);
		Debug.Log ("You died");
	}

}
