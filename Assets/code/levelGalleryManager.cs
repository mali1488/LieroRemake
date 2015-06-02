using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelGalleryManager : MonoBehaviour
{
	public Image currentImg;
	public Sprite[] mySprites;
	private int currentLevel;

	void Start () {
		currentLevel = 0;
		PlayerPrefs.SetInt ("level",currentLevel + 1);
		currentImg.sprite = mySprites [currentLevel];
	}

	public void changeLevel () {
		if (currentLevel + 1 >= mySprites.Length) {
			currentLevel = 0;
		} else {
			currentLevel++;
		}
		Debug.Log ("currentlevel: " + currentLevel);
		currentImg.sprite = mySprites [currentLevel];
		//PlayerPrefs.SetString ("level", "level" + currentLevel);
	}

	public void saveLevel() {
		PlayerPrefs.SetInt ("level",currentLevel + 1);
	}

}
