using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public Transform player;
	public Transform aim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		//Debug.Log (mouse_x);
		float player_x = player.position.x;
		Vector3 screenMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex,mousey,0));
		Debug.Log (screenMousePosition.x);

		aim.position = new Vector3 (screenMousePosition.x, screenMousePosition.y ,0);
		Debug.Log (player_x);
	}
}
