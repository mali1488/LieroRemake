using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class gameSettingsTranslate : MonoBehaviour {

	public Text selectLevel;
	public Text selectWeapons;
	public Text play;
	public Text back;

	public translation translate;

	void Start () {
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
