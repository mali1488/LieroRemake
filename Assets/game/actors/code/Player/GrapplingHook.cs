using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	private Rigidbody2D rope;
	private bool hooked = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shootHook(Aim aim) {
		Debug.Log("Shooting Hook");
		hooked = true;



	}
}
