using UnityEngine;
using System.Collections;

public class RemoveEarth : MonoBehaviour {

	
	public GameObject Spawn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 0.3f);
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		Destroy (gameObject);
		if (Spawn != null) {
			var contact0 = collision.contacts [0];
			Instantiate (Spawn, contact0.point, transform.rotation);
		}
	}
}