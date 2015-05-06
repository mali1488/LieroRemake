using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class translation : MonoBehaviour {

	public enum language {
		ENG,
		SWE
	}
	
	public language choosenLanguage;
	Dictionary<language,Dictionary<string,string>> translate;
	Dictionary<string,string> swe;
	Dictionary<string,string> eng;
	
	public mainMenuTranslate mainMenu;
	public settingsMenuTranslate settingsMenu;
	public levelSelectTranslate levelSelect;
	public gameSettingsTranslate gameSettings;
	public weaponsTranslate weapons;

	void Awake () {
		choosenLanguage = language.SWE;
		swe = new Dictionary<string, string>();
		eng = new Dictionary<string, string>();
		translate = new Dictionary<language, Dictionary<string,string>>();
		translate.Add (language.ENG,eng);
		translate.Add (language.SWE,swe);

		// main menu texts
		eng.Add("multiplayer","Multiplayer");
		eng.Add("settings","Settings");
		eng.Add("quitGame","Quit");
		swe.Add("multiplayer","Flera spelare");
		swe.Add("settings","Inställningar");
		swe.Add("quitGame","Avsluta");

		// settings texts
		swe.Add ("language","Språk");
		swe.Add ("amountOfBlood","Blod mängd");
		swe.Add ("lives","Antal liv");
		swe.Add ("regenLife","Regenerera hälsa");
		swe.Add ("controls","Controller");
		swe.Add ("back","Tillbaka");
		eng.Add ("language","Language");
		eng.Add ("amountOfBlood","Amount of blood");
		eng.Add ("lives","Lives");
		eng.Add ("regenLife","Regenerate life");
		eng.Add ("controls","Controler");
		eng.Add ("back","Back");

		// level select texts
		swe.Add ("nextLevel","Bästa bana");
		swe.Add ("saveLevel", "Spara");
		eng.Add ("nextLevel","Next level");
		eng.Add ("saveLevel", "Save");

		// Game settings texts
		swe.Add ("selectLevel","Välj bana");
		swe.Add ("selectWeapons","Välj vapen");
		swe.Add ("play","Spela");
		eng.Add ("selectLevel", "Select level");
		eng.Add ("selectWeapons","Select weapons");
		eng.Add ("play","Play");

		// Weapons texts
		swe.Add ("save","Spara");
		eng.Add ("save","Save");
	}

	void Start() {

	}

	public Dictionary<string,string> getDict() {
		return translate[choosenLanguage];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void translateButtons() {
		mainMenu.translateText ();
		settingsMenu.translateText ();
		levelSelect.translateText ();
		gameSettings.translateText ();
		weapons.translateText ();
	}
}
