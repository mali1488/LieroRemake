// Variables to determine which button is clicked
var quitButton=false;
var settingButton=false;

function OnMouseEnter(){
	//change text color
	GetComponent.<Renderer>().material.color=new Color(0.0, 0.8, 0.8); //Cyan color in rgb;
}

function OnMouseExit(){
	//change text color
	GetComponent.<Renderer>().material.color=Color.white;
}

function OnMouseUp(){

	if (quitButton==true) {
		Application.Quit();
	} else if(settingButton==true) {
		Debug.Log ("Settings");
		// Load new setting menu. Later use :
		/* PlayerPrefs.SetInt("HiScore",0); to save scene to scene variables  
				and 
		   PlayerPrefs.GetInt("HiScore"); to get that variable in another scene
		*/
	} else {
		// This loads the MainScene 
		Application.LoadLevel("level1");
	}
}

function Update(){
	//quit game if escape key is pressed
	if (Input.GetKey(KeyCode.Escape)) { 
		Application.Quit();
	} else if (Input.GetKey(KeyCode.DownArrow)) {
		Debug.Log("move down");
	} else if (Input.GetKey(KeyCode.UpArrow)) {
		Debug.Log("move up");
	}
}
