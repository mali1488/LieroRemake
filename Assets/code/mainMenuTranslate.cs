using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class mainMenuTranslate : MonoBehaviour {

	public Text multiplayer;
	public Text settings;
	public Text quitGame;

	public translation translate;

	void Start () {
		translateText ();
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		multiplayer.text = s["multiplayer"];
		settings.text = s["settings"];
		quitGame.text = s["quitGame"];
	}
}
