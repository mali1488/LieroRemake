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

	// Use this for initialization
	void Start () {
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
