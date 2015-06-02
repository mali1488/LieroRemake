using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class mainMenuTranslate : MonoBehaviour {

	public Text multiplayer;
	public Text settings;
	public Text quitGame;
	
	public Button multiplayerButton;
	public Button tutorialButton;
	public Button settingsButton;
	public Button quitButton;

	public RectTransform rect;

	public translation translate;

	void Start () {
		// The resoulution I build the GUI in
		Vector2 referenceResolution = new Vector2 (685, 397);
		
		// Get the factor on how much to scale
		float scaleFactorX = Screen.width / referenceResolution.x;
		float scaleFactorY = Screen.height / referenceResolution.y;

		// Abuse the fact that every button has the same size
		float scaleX = multiplayerButton.image.rectTransform.sizeDelta.x * scaleFactorX;
		float scaleY = multiplayerButton.image.rectTransform.sizeDelta.y * scaleFactorY;
		
		

		// Resize every button so it correspons to the current resolution
		multiplayerButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		tutorialButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		settingsButton.image.rectTransform.sizeDelta = new Vector2 (scaleX, scaleY);
		quitButton.image.rectTransform.sizeDelta = new Vector2(scaleX,scaleY);

		// calculate offset Y position
		float offset = tutorialButton.image.rectTransform.position.y - multiplayerButton.image.rectTransform.position.y;
		offset *= scaleFactorY;

		// Set new positions for evety button
		Vector3 prevPos = tutorialButton.image.rectTransform.position;
		Vector3 currentPos = multiplayerButton.image.rectTransform.position;
		multiplayerButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = multiplayerButton.image.rectTransform.position;;
		currentPos = settingsButton.image.rectTransform.position;
		settingsButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = settingsButton.image.rectTransform.position;
		currentPos = quitButton.image.rectTransform.position;
		quitButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		translateText ();
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		multiplayer.text = s["multiplayer"];
		settings.text = s["settings"];
		quitGame.text = s["quitGame"];
	}
}