using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChangeWeapon : MonoBehaviour {
	public IList<string> weaponList = new List<string>();
	public Sprite[] testList;
	public int bulletDamage = 0;
	public Texture weaponTexture;
	public Texture bulletTexture;
	public GameObject arrow;
	// Use this for initialization
	void Start () {
		testList = Resources.LoadAll<Sprite>("Textures");
		//arrow.GetComponent<SpriteRenderer>().sprite = testList[0];
	
		//for testing purposes
		PlayerPrefs.SetInt("ak47", 1);
		
		PlayerPrefs.SetInt("bazooka", 1);
		
		PlayerPrefs.SetInt("pistol", 1);
		
		PlayerPrefs.SetInt("grenade", 1);
		
		PlayerPrefs.SetInt("shotgun", 1);
		//

		if (PlayerPrefs.GetInt("bazooka") == 1) {

			bulletDamage = 20;
			weaponList.Add("bazooka");
		}

		if (PlayerPrefs.GetInt("pistol") == 1) {
			bulletDamage = 4;
			weaponList.Add("pistol");
		}

		if (PlayerPrefs.GetInt("grenade") == 1) {
			bulletDamage = 10;
			weaponList.Add("grenade");
		}

		if (PlayerPrefs.GetInt("shotgun") == 1) {
			bulletDamage = 5;
			weaponList.Add("shotgun");
		}

		if (PlayerPrefs.GetInt("ak47") == 1) {
			bulletDamage = 5;
			weaponList.Add("ak47");

		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	
	public void swapWeapon(int weaponNumber){
		Debug.Log ("New Weapon is: " + weaponList[weaponNumber]);
		
		//arrow.GetComponent<SpriteRenderer>().sprite = testList[weaponNumber];
		
	}

}
