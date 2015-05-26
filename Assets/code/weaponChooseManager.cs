using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class weaponChooseManager : MonoBehaviour {

	public Toggle bazooka;
	public Toggle pistol;
	public Toggle grenade;
	public Toggle angryChicken;
	public Toggle ak47;
	public Toggle shotgun;
	public Toggle flamethrower;

	private void toggle(Toggle toggle, string key) {
		if (toggle.isOn) {
			PlayerPrefs.SetInt (key, 1);
		} else {
			PlayerPrefs.SetInt(key,0);
		}
	}

	public void chooseWeapon(string type) {
		switch (type) {
			case "bazooka":
				toggle (bazooka,"bazooka");
				break;
			case "pistol":
				toggle (pistol, "pistol");
				break;
			case "grenade":
				toggle (grenade,"grenade");
				break;
			case "angryChicken":
				toggle (angryChicken,"angryChicken");
				break;
			case "shotgun":
				toggle (shotgun,"shotgun");
				break;
			case "flamethrower":
				toggle (flamethrower,"flamethrower");
				break;
			case "ak47":
				toggle (ak47,"ak47");
				break;
			default:
				Debug.Log ("Something went wrong, weaponChooseManager");
			break;
		}
		Debug.Log (PlayerPrefs.GetInt("bazooka"));
	}
}
