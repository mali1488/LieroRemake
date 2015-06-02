using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonManager : MonoBehaviour {

	public Scrollbar bloodBar;
	public Scrollbar livesBar;
	public Text bloodText;
	public Text livesText;
	public Toggle regenerateLife;
	public translation translationTool;

	// Player one inputs
	public InputField jump;
	public InputField shoot;
	public InputField left;
	public InputField right;
	public InputField up;
	public InputField down;

	// Player two inputs
	public InputField jump2;
	public InputField shoot2;
	public InputField left2;
	public InputField right2;
	public InputField up2;
	public InputField down2;

	public void Awake() {
		PlayerPrefs.SetString ("jump","space");
		PlayerPrefs.SetString ("shoot","z");
		PlayerPrefs.SetString ("right","d");
		PlayerPrefs.SetString ("left","a");
		PlayerPrefs.SetString ("up","w");
		PlayerPrefs.SetString ("down","s");
		PlayerPrefs.SetString ("next","e");
		PlayerPrefs.SetString ("prev","q");

		PlayerPrefs.SetString ("jump2","m");
		PlayerPrefs.SetString ("shoot2","n");
		PlayerPrefs.SetString ("right2","y");
		PlayerPrefs.SetString ("left2","k");
		PlayerPrefs.SetString ("up2","o");
		PlayerPrefs.SetString ("down2","l");
	}
	
	public void back() {
		Debug.Log ("Go back");
	}

	public void changeLanguage() {
		if (translationTool.choosenLanguage == translation.language.ENG) {
			translationTool.choosenLanguage = translation.language.SWE;
		} else {
			translationTool.choosenLanguage = translation.language.ENG;
		}
		
	}

	public void play() {
		int level = PlayerPrefs.GetInt ("level");
		Debug.Log ("loading level: " + level);
		Application.LoadLevel ("level" + level);
	}

	public void playTutorial(){
		Application.LoadLevel ("tutorialLevel");
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

	public void checkForSpace(string button, InputField field) {
		if (field.text == " ") {
			field.text = "space";
			PlayerPrefs.SetString(button, field.text);
		}
	}

	public void setControllButton(string button, InputField field) {
		PlayerPrefs.SetString(button,field.text);
		Debug.Log(button +": "+ PlayerPrefs.GetString(button));
		checkForSpace(button, field);

		Debug.Log ("A: " + KeyCode.A);
	}

	public void quit() {
		Application.Quit ();
	}

	public void setControl(string button) {
		Debug.Log ("set: " + button);
		switch (button) {
			case "right":
				setControllButton(button, jump);	
				break;
			case "jump":
				setControllButton(button, jump);
				break;
			case "shoot":
				setControllButton(button, shoot);
				break;
			case "left":
				setControllButton(button, left);
				break;
			case "up":
				setControllButton(button, up);
				break;
			case "down":
				setControllButton(button, down);
				break;
			case "right2":
				setControllButton(button, right2);
				break;
			case "jump2":
				setControllButton(button, jump2);
				break;
			case "shoot2":
				setControllButton(button, shoot2);
				break;
			case "left2":
				setControllButton(button, left2);
				break;
			case "up2":
				setControllButton(button, up2);
				break;
			case "down2":
				setControllButton(button, down2);
				break;
			default:
				Debug.Log("default");
				break;
		}
	}
}
