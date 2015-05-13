using UnityEngine;
using System.Collections;

public class bulletGravity : MonoBehaviour {
	public Rigidbody2D r;
	public float angle;
	public GameObject Spawn;
	public float SpawnCooldown = 1.0F;

	public void set(float angle,bool isFacingRight,float speed){
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		if (isFacingRight) {
			r.AddForce(new Vector2(speed, speed*Mathf.Sin(angle)));
		} else {
			r.AddForce(new Vector2(-speed, speed*Mathf.Sin(angle)));
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		Destroy (gameObject);
		if (Spawn != null) {
			Debug.Log ("spawn");
			var contact0 = collision.contacts [0];
			Instantiate (Spawn, contact0.point, transform.rotation);
		}

	}
}
