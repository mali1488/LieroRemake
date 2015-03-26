using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float jumpHeigt = 2.0F;
	public float acc = 0.01F;
	public float pos_x;
	public float pos_y;
	bool isFalling = false;

	void jump () {
		if ((Input.GetKeyDown (KeyCode.Space)) && this.isFalling) {
			pos_x = transform.position.x;
			pos_y = transform.position.y;
			transform.position = new Vector2(pos_x,pos_y + this.jumpHeigt* 0.2F);
			this.isFalling = false;
		} 
		this.isFalling = true;
	}

	void movePlayer() {
		float moveX = Input.GetAxis ("Horizontal");
		transform.position = new Vector3 (transform.position.x + moveX * acc, transform.position.y,0);
	}
	// Update is called once per frame
	void Update () {
		jump ();
		movePlayer ();
		
	}

	public void OnTriggerEnter(Collider c) {
		Debug.Log("Collided!");
	}
}
