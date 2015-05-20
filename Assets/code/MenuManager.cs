using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu CurrentMenu;
	public translation translationTool;

	public void Start() {
		ShowMenu (CurrentMenu);
		translationTool = new translation ();
	}

	public void ShowMenu(Menu menu) {
		Debug.Log ("Change menu");
		if(CurrentMenu != null) {
			// Hide the current menu
			CurrentMenu.IsOpen = false;
		}
		// Change the menu
		CurrentMenu = menu;
		// Set the new menu visible
		CurrentMenu.IsOpen = true;
	}
}
