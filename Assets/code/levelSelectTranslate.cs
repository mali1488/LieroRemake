using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class levelSelectTranslate : MonoBehaviour {
	public translation translate;
	public Text saveLevel;
	public Text nextLevel;

	public Button saveButton;
	public Button nextButton;
	
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
		nextButton.image.rectTransform.sizeDelta = new Vector2 (scaleX, scaleY);

		translateText ();
	}
	
	public void translateText () {
		Dictionary<string,string> s = translate.getDict ();
		saveLevel.text = s["saveLevel"];
		nextLevel.text = s["nextLevel"];
	}
}
