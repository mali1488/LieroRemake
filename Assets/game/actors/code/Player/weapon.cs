using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour {
	
	public Rigidbody2D bullet;
	private float length = 10.0F;
	private float damage;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void shoot(Vector3 playerPos, float angle, Vector3 aimPos,Quaternion  playerRotation,bool isFacingRight) {
		//Debug.Log ("shoot");
		float speed = 1000.0F;
		GameObject i = (GameObject)Instantiate (Resources.Load("bullet"), new Vector3(playerPos.x + length*Mathf.Cos (angle), playerPos.y + length*Mathf.Sin(angle),playerPos.z),playerRotation);

		i.GetComponent<bulletGravity> ().set (angle,isFacingRight,speed);
		//i.GetComponent<bulletNoGravity> ().set (angle,isFacingRight,speed);

	}
}
