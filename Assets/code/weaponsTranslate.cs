using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class weaponsTranslate : MonoBehaviour {

	public Text save;

	public translation translate;

	void Start () {
		translateText ();	
	}

	public void translateText() {
		Dictionary<string,string> s = translate.getDict ();
		save.text = s["save"];
	}
}
