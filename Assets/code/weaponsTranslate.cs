using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class weaponsTranslate : MonoBehaviour {

	public Text save;

	public Button saveButton;
	public Button pistol;
	public Button shotgun;
	public Button flamethrower;
	public Button grenade;
	public Button ak47;
	public Button angryChicken;
	public Button bazooka;

	public translation translate;

	void Start () {
		// The resoulution I build the GUI in
		Vector2 referenceResolution = new Vector2 (685, 397);
		
		// Get the factor on how much to scale
		float scaleFactorX = Screen.width / referenceResolution.x;
		float scaleFactorY = Screen.height / referenceResolution.y;

		// Abuse the fact that every button has the same size
		float scaleX = saveButton.image.rectTransform.sizeDelta.x * scaleFactorX;
		float scaleY = saveButton.image.rectTransform.sizeDelta.y * scaleFactorY;

		// Resize every button so it correspons to the current resolution
		saveButton.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		pistol.image.rectTransform.sizeDelta = new Vector2 (scaleX, scaleY);
		shotgun.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		flamethrower.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		grenade.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		ak47.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		angryChicken.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		bazooka.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);

		// calculate offset Y position
		float offset = bazooka.image.rectTransform.position.y - ak47.image.rectTransform.position.y;
		offset *= scaleFactorY;
		
		// Set new positions for every button
		Vector3 prevPos = bazooka.image.rectTransform.position;
		Vector3 currentPos = ak47.image.rectTransform.position;
		ak47.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = ak47.image.rectTransform.position;
		currentPos = pistol.image.rectTransform.position;
		pistol.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		
		prevPos = pistol.image.rectTransform.position;
		currentPos = shotgun.image.rectTransform.position;
		shotgun.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = shotgun.image.rectTransform.position;
		currentPos = flamethrower.image.rectTransform.position;
		flamethrower.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = flamethrower.image.rectTransform.position;
		currentPos = grenade.image.rectTransform.position;
		grenade.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		prevPos = grenade.image.rectTransform.position;
		currentPos = angryChicken.image.rectTransform.position;
		angryChicken.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);

		translateText ();	
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		save.text = s["save"];
	}
}
