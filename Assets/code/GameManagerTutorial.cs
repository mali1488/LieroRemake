﻿using UnityEngine;
using System.Collections;

public class GameManagerTutorial : MonoBehaviour {
	
	
	public GameObject spawnedObject;
	//TODO: make game manager a singleton
	void Start () {
		StartCoroutine(spawn());
	}
	
	IEnumerator spawn() {
		// The real keybindings choosen from the main menu
		/*
      moveRightPlayer1 = PlayerPrefs.GetString ("right");
      moveLeftPlayer1 = PlayerPrefs.GetString ("left");
      aimUpPlayer1 = PlayerPrefs.GetString ("up");
      aimDownPlayer1 = PlayerPrefs.GetString ("down");
      prevWeaponPlayer1 = PlayerPrefs.GetString ("next");
      nextWeaponPlayer1 = PlayerPrefs.GetString ("prev");
      shootPlayer1 = PlayerPrefs.GetString ("shoot");
      jump1 = PlayerPrefs.GetString ("jump");

      moveRightPlayer2 = PlayerPrefs.GetString ("right2");
      moveLeftPlayer2 = PlayerPrefs.GetString ("left2");
      aimUpPlayer2 = PlayerPrefs.GetString ("up2");
      aimDownPlayer2 = PlayerPrefs.GetString ("down2");
      shootPlayer2 = PlayerPrefs.GetString ("shoot2");
    */
		GameObject player1 = Instantiate(spawnedObject);
		player1.GetComponent<PlayerTutorial>().Setup("a", "d", "w", "s", "q", "e", "z", "space", "f", -140, 63, 0, 0, 0.5f, 1.0f, true);
		GameObject player2 = Instantiate(spawnedObject);
		player2.GetComponent<PlayerTutorial>().Setup("left", "right", "up", "down", "k", "l", "m", "n", "b", 152, 63, 0.5f, 0, 0.5f, 1.0f, false);
		yield return null;
	}
}