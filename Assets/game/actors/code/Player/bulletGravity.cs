using UnityEngine;
using System.Collections;

public class bulletGravity : MonoBehaviour {
	public Rigidbody2D r;
	public float angle;
	// Use this for initialization
	void Start () {
		
	}
	
	public void set(float angle,bool isFacingRight,float speed){
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		if (isFacingRight) {
			r.AddForce(new Vector2(speed, speed*Mathf.Sin(angle)));
		} else {
			r.AddForce(new Vector2(-speed, speed*Mathf.Sin(angle)));
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
