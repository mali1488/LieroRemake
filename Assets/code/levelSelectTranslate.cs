using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class levelSelectTranslate : MonoBehaviour {
	public translation translate;
	public Text saveLevel;
	public Text nextLevel;
	// Use this for initialization
	void Start () {
		translateText ();
	}
	
	public void translateText () {
		Dictionary<string,string> s = translate.getDict ();
		saveLevel.text = s["saveLevel"];
		nextLevel.text = s["nextLevel"];
	}
}
