using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class playerControllsTranslate : MonoBehaviour {

	public Button right;
	public Button left;
	public Button jump;
	public Button shoot;
	public Button up;
	public Button down;
	public Button save;

	// player one input fields
	public InputField rightInput;
	public InputField leftInput;
	public InputField jumpInput;
	public InputField shootInput;
	public InputField upInput;
	public InputField downInput;
	
	// player two input fields
	public InputField rightInput2;
	public InputField leftInput2;
	public InputField jumpInput2;
	public InputField shootInput2;
	public InputField upInput2;
	public InputField downInput2;

	void Start () {
		// The resoulution I build the GUI in
		Vector2 referenceResolution = new Vector2 (685, 397);
		
		// Get the factor on how much to scale
		float scaleFactorX = Screen.width / referenceResolution.x;
		float scaleFactorY = Screen.height / referenceResolution.y;
		
		// Abuse the fact that every button has the same size
		float scaleX = right.image.rectTransform.sizeDelta.x * scaleFactorX;
		float scaleY = right.image.rectTransform.sizeDelta.y * scaleFactorY;
		
		// Resize every button so it correspons to the current resolution
		right.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		left.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		jump.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		shoot.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		up.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		down.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);
		save.image.rectTransform.sizeDelta = new Vector2 (scaleX,scaleY);

		// calculate offset Y position
		float offset = right.image.rectTransform.position.y - left.image.rectTransform.position.y;
		offset *= scaleFactorY;
		float offsetx = scaleFactorX;
		
		// Set new positions for every button
		Vector3 prevPos = right.image.rectTransform.position;
		Vector3 currentPos = left.image.rectTransform.position;

		rightInput.image.rectTransform.position = new Vector3 (rightInput.image.rectTransform.position.x + offsetx, rightInput.image.rectTransform.position.y,rightInput.image.rectTransform.position.z);
		rightInput2.image.rectTransform.position = new Vector3 (rightInput2.image.rectTransform.position.x + offsetx, rightInput.image.rectTransform.position.y,rightInput.image.rectTransform.position.z);

		left.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		leftInput.image.rectTransform.position = new Vector3 (leftInput.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);
		leftInput2.image.rectTransform.position = new Vector3 (leftInput2.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);

		prevPos = left.image.rectTransform.position;
		currentPos = jump.image.rectTransform.position;
		jump.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		jumpInput.image.rectTransform.position = new Vector3 (jumpInput.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);
		jumpInput2.image.rectTransform.position = new Vector3 (jumpInput2.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);

		prevPos = jump.image.rectTransform.position;
		currentPos = shoot.image.rectTransform.position;
		shoot.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		shootInput.image.rectTransform.position = new Vector3 (shootInput.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);
		shootInput2.image.rectTransform.position = new Vector3 (shootInput2.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);

		prevPos = shoot.image.rectTransform.position;
		currentPos = up.image.rectTransform.position;
		up.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		upInput.image.rectTransform.position = new Vector3 (upInput.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);
		upInput2.image.rectTransform.position = new Vector3 (upInput2.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);

		prevPos = up.image.rectTransform.position;
		currentPos = down.image.rectTransform.position;
		down.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
		downInput.image.rectTransform.position = new Vector3 (downInput.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);
		downInput2.image.rectTransform.position = new Vector3 (downInput2.image.rectTransform.position.x + offsetx, prevPos.y - offset, currentPos.z);

		prevPos = down.image.rectTransform.position;
		currentPos = save.image.rectTransform.position;
		save.image.rectTransform.position = new Vector3 (currentPos.x, prevPos.y - offset, currentPos.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
