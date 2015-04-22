using UnityEngine;
using System.Collections;

public class menuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void test() {
		Debug.Log ("Quit application");
		Animator a = GetComponent<Animator> ();
		a.SetBool ("Open",false);
		Application.Quit ();
	}
}
