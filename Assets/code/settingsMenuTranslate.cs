using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class settingsMenuTranslate : MonoBehaviour {
	public translation translate;

	public Text languageText;
	public Text amountOfBlood;
	public Text lives;
	public Text regenLife;
	public Text controls;
	public Text back;

	public Button languageButton;
	public Button amountOfBloodButton;
	public Button livesButton;
	public Button regenLifeButton;
	public Button controlsButton;
	public Button backButton;

	public Transform thisMenu;
	// Use this for initialization
	void Start () {
		// The resoulution I build the GUI in
		Vector2 referenceResolution = new Vector2 (685, 397);
		thisMenu.position = new Vector3 (0.0F,0.0F,0.0F);
		// Get the factor on how much to scale
		float scaleFactorX = Screen.width / referenceResolution.x;
		float scaleFactorY = Screen.height / referenceResolution.y;

		// Abuse the fact that every button has the same size
		float scaleX = languageButton.image.rectTransform.sizeDelta.x * scaleFactorX;
		float scaleY = languageButton.image.rectTransform.sizeDelta.y * scaleFactorY;

		// Resize every button so it correspons to the current resolution
		languageButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		amountOfBloodButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		livesButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		regenLifeButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		controlsButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		backButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);

		// calculate offset Y position
		float offset = languageButton.image.rectTransform.position.y - 	amountOfBloodButton.image.rectTransform.position.y;
		offset *= scaleFactorY;

		// Set new positions for evety button
		Vector3 prevPos = languageButton.image.rectTransform.position;
		Vector3 currentPos = amountOfBloodButton.image.rectTransform.position;
		amountOfBloodButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = amountOfBloodButton.image.rectTransform.position;
		currentPos = livesButton.image.rectTransform.position;
		livesButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = livesButton.image.rectTransform.position;
		currentPos = regenLifeButton.image.rectTransform.position;
		regenLifeButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = regenLifeButton.image.rectTransform.position;
		currentPos = controlsButton.image.rectTransform.position;
		controlsButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = controlsButton.image.rectTransform.position;
		currentPos = backButton.image.rectTransform.position;
		backButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		translateText ();
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		languageText.text = s ["language"];
		amountOfBlood.text = s ["amountOfBlood"];
		lives.text = s ["lives"];
		regenLife.text = s ["regenLife"];
		controls.text = s ["controls"];
		back.text = s ["back"];
	}
}
