using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonManager : MonoBehaviour {

	public Scrollbar bloodBar;
	public Scrollbar livesBar;
	public Text bloodText;
	public Text livesText;
	public Toggle regenerateLife;

	public void back() {
		Debug.Log ("Go back");
	}

	public void play() {
		int level = PlayerPrefs.GetInt ("level");
		Application.LoadLevel ("level" + level);
	}

	private void bloodScrollBar() {
		// get bar value
		float value = bloodBar.value;
		// convert ti interger and procent
		int toProcent = (int)(value * 100);
		// change the text in the view
		bloodText.text = toProcent.ToString() + "%";
		// set playerpref
		PlayerPrefs.SetInt ("blood", toProcent);
	}

	public void OnSubmit() {
		if (regenerateLife.isOn) { 
			PlayerPrefs.SetInt("regenerateLife", 1);
		} else {
			PlayerPrefs.SetInt("regenerateLife",0);
		}
		Debug.Log (PlayerPrefs.GetInt("regenerateLife"));
	}

	public void livesScrollBar() {
		float value = livesBar.value;
		int toProcent = (int)(value * 100);
		livesText.text = toProcent.ToString();
		PlayerPrefs.SetInt ("lives", toProcent);
	}

	public void OnGUI() {
		bloodScrollBar ();
		livesScrollBar ();
		
	}
}
