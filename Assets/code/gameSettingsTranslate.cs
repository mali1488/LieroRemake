using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class gameSettingsTranslate : MonoBehaviour {

	public Text selectLevel;
	public Text selectWeapons;
	public Text play;
	public Text back;

	public Button selectLevelButton;
	public Button selectWeaponsButton;
	public Button playButton;
	public Button backButton;

	public translation translate;

	void Start () {
		// The resoulution I build the GUI in
		Vector2 referenceResolution = new Vector2 (685, 397);
		
		// Get the factor on how much to scale
		float scaleFactorX = Screen.width / referenceResolution.x;
		float scaleFactorY = Screen.height / referenceResolution.y;
		
		// Abuse the fact that every button has the same size
		float scaleX = playButton.image.rectTransform.sizeDelta.x * scaleFactorX;
		float scaleY = playButton.image.rectTransform.sizeDelta.y * scaleFactorY;
		
		// Resize every button so it correspons to the current resolution
		playButton.image.rectTransform.sizeDelta = new Vector2(scaleX,scaleY);
		selectLevelButton.image.rectTransform.sizeDelta = new Vector2(scaleX,scaleY);
		selectWeaponsButton.image.rectTransform.sizeDelta = new Vector2(scaleX,scaleY);
		backButton.image.rectTransform.sizeDelta = new Vector2(scaleX,scaleY);

		// calculate offset Y position
		float offset = selectLevelButton.image.rectTransform.position.y - 	selectWeaponsButton.image.rectTransform.position.y;
		offset *= scaleFactorY;
		
		// Set new positions for every button
		Vector3 prevPos = selectLevelButton.image.rectTransform.position;
		Vector3 currentPos = selectWeaponsButton.image.rectTransform.position;
		selectWeaponsButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = selectWeaponsButton.image.rectTransform.position;
		currentPos = playButton.image.rectTransform.position;
		playButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = playButton.image.rectTransform.position;
		currentPos = backButton.image.rectTransform.position;
		backButton.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		translateText ();
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		selectLevel.text = s["selectLevel"];
		selectWeapons.text = s["selectWeapons"];
		play.text = s["play"];
		back.text = s["back"];
	}
}
