using UnityEngine;
using System.Collections;
using Spine;
using System;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour {

  public GameObject boneFollower;
  public bool _isFacingRight;
  private CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;

  public float fireRate = 0.5F;
  private float nextFire = 0.0F;
  private bool isAimOnce = false;
  private bool isIgnoreFirstShot = true;


  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;

  private Aim aim = null;

  //Input variables player
  private string moveRight = "d";
  private string moveLeft = "a";
  private string aimUp = "w";
  private string aimDown = "s";
  private string prevWeapon = "q";
  private string nextWeapon = "e";
  private string shoot = "z";
  private string jump = "space";
  private string digging = "f";

  //Weapon variables
  private WeaponHolster weaponHolster;
  private Weapon weapon;


  //digging
  private Digging dig;

  //camera
  private Camera cam;
  private Rect tempCam;

  //Text Turorial
  private GameObject tempText;
  private bool player1 = false;
  private Text tutorialText;
  private bool rotated = false;
  private bool text0 = false;
  private bool text1 = false;
  private bool text2 = false;
  private bool text3 = false;
  private bool text4 = false;
  private bool text5 = false;
  private int counter = 0;
  private bool earth = false;
  //Spine animation

  private AnimPlayer animPlayer = null;
  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(String moveLeft, String moveRight, String aimUp, String aimDown, String prevWeapon, String nextWeapon, String shoot, String jump, String digging, int positionX, int positionY, float camX, float camY, float camWidth, float camHeight, bool p) {

    cam = GetComponent<Camera> ();
    this.moveRight = moveRight;
    this.moveLeft = moveLeft;
    this.aimUp = aimUp;
    this.aimDown = aimDown;
    this.prevWeapon = prevWeapon;
    this.nextWeapon = nextWeapon;
    this.shoot = shoot;
    this.jump = jump;
    this.digging = digging;
    transform.position = new Vector2(positionX, positionY);
    tempCam.height = camHeight;
    tempCam.width = camWidth;
    tempCam.y = camY;
    tempCam.x = camX;
    GameObject tempChild = this.transform.GetChild (1).gameObject;
    tempChild.GetComponent<Camera> ().rect = tempCam;
    player1 = p;

  }

  public void Start() {
    _controller = GetComponent<CharacterController2D>();
    dig = GetComponent<Digging>();
    _isFacingRight = transform.localScale.x > 0;


    Debug.Log (tempText);

    skeletonAnimation = GetComponent<SkeletonAnimation>();

    if (skeletonAnimation) {
      aim = GetComponent<Aim>();
      if (aim) {
        aim.Setup(skeletonAnimation);
      }
      weaponHolster = GetComponent<WeaponHolster>();
      if (weaponHolster) {
        weaponHolster.Setup(skeletonAnimation);
        weapon = weaponHolster.getCurrentWeapon();
      }
      animPlayer = GetComponent<AnimPlayer>();
      if (animPlayer) {
        animPlayer.Setup(skeletonAnimation);
      }
    }
  }

  public void Update() {
    HandleInput();
    var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
    _controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
    //aim.aiming (_isFacingRight, _controller.transform.position);
    //tempText.transform.position = new Vector3 (tutorialCanvas.transform.position.x, tutorialCanvas.transform.position.y + 200f, tutorialCanvas.transform.position.z);


    GameObject tempChild2 = this.transform.GetChild (1).gameObject;
    Camera tempCam2 = tempChild2.GetComponent<Camera> ();
    //Vector3 charPos = Camera.current.WorldToScreenPoint (this.transform.position);
    Vector3 charPos = tempCam2.WorldToScreenPoint (this.transform.position);
    GameObject tempCanvas = this.transform.GetChild (2).gameObject;
    tempText = tempCanvas.transform.GetChild (0).gameObject;
    tempText.transform.position = new Vector3(charPos.x+50f, charPos.y+100f,charPos.z);
    tutorialText = tempText.GetComponent<Text>();
	if (!text0 ) {
			tutorialText.text = "Welcome to the tutorial!";
			text0 = true;
		} else if (counter == 150 && !earth) {
			tutorialText.text = "Player1 is to the left";
		} else if (counter == 300 && !earth) {
			tutorialText.text = "Player2 is to the right";
		} else if (counter == 400 && !earth) {
			tutorialText.text = "First key is Player1's";
		} else if (counter == 550 && !earth) {
			tutorialText.text = "Example: Press T or P";
		} else if (counter == 700 && !earth) {
			tutorialText.text = "T interacts with Player1";
		} else if (counter == 850 && !earth) {
			tutorialText.text = "P interacts with Player2";
		} else if (counter == 1000 && !earth) {
			tutorialText.text = "Lets Try!";
		} else if (counter == 1150 && !earth) {
			tutorialText.text = "Press A or Left Arrow";
		} 
	if (counter < 2000) {
			counter += 1;
		}

	if (earth) {
		
		if (counter == 150) {
			tutorialText.text = "Move Left: A or Left Arrow";
		} else if (counter == 300) {
			tutorialText.text = "Move right: D or Right Arrow";
		} else if (counter == 450) {
			tutorialText.text = "When you find the Earth";
		} else if (counter == 600) {
				tutorialText.text = "Stand close to it!";
		} else if (counter == 750) {
			tutorialText.text = "Press F or B to Dig";
		}else if (counter == 950) {
			tutorialText.text = "Both Diggin and Shooting";	
		}else if (counter == 1050) {
			tutorialText.text = "Removes brown Earth";	
		}else if (counter == 1200) {
				tutorialText.text = "Shooting a Player hurts them";	
		}else if (counter == 1350) {
			tutorialText.text = "You Can also change Weapons";		
		}else if (counter == 1450) {
			tutorialText.text = "With Q and E for Player1";		
		}else if (counter == 1600) {
			tutorialText.text = "Or K and L for Player2";	
		}else if (counter == 1900) {
			tutorialText.text = "To Exit Press Escape!";	
		}
	}
    tempText.transform.position = new Vector3(charPos.x, charPos.y+100f,charPos.z);

  }

  private void HandleInput() {
	
    GameObject tempChild2 = this.transform.GetChild (1).gameObject;
    Camera tempCam2 = tempChild2.GetComponent<Camera> ();
    //Vector3 charPos = Camera.current.WorldToScreenPoint (this.transform.position);
    Vector3 charPos = tempCam2.WorldToScreenPoint (this.transform.position);
    GameObject tempCanvas = this.transform.GetChild (2).gameObject;
    tempText = tempCanvas.transform.GetChild (0).gameObject;
    tempText.transform.position = new Vector3(charPos.x+50f, charPos.y+100f,charPos.z);
    tutorialText = tempText.GetComponent<Text>();

    if (Input.GetKey(moveRight)) {
		if(!text2){
			
				text2 = true;
				tutorialText.text = "Press space or N!";
			}

      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
      }
      if (_controller.State.IsGrounded){
        animPlayer.Move();
      }

      //MOVE LEFT
    } else if (Input.GetKey(moveLeft)) {
			if(!text1){
				text1 = true;
				tutorialText.text = "Press D or Right Arrow!";
			}

    	_normalizedHorizontalSpeed = -1;
      if(_isFacingRight) {
        Flip();
      }
      if (_controller.State.IsGrounded) {
        animPlayer.Move();
      }
      //IDLE
    } else {
      _normalizedHorizontalSpeed = 0;
      if (_controller.State.IsGrounded && !Input.GetKey(shoot)) {
        animPlayer.Idle();
      }

    }
    if (_controller.CanJump && Input.GetKeyDown(jump)) {
			if(!text3){
				tutorialText.text = "Press and Hold Z or M";
				text3 = true;
			}
      _controller.Jump();
      animPlayer.Jump();
    }

    if (Input.GetKey(aimUp)) {
			
			if(!text5){
				tutorialText.text = "Press S or Down Arrow";
				text5 = true;
			}
      aim.Up();
    }

    if (Input.GetKey(aimDown)) {
			if(!earth){
				earth = true;
				counter = 0;
				tutorialText.text = "Find the brown earth!";
			}
      aim.Down();
    }

    if (Input.GetKey(digging)) {
      dig.Dig(boneFollower.transform.position, boneFollower.transform.eulerAngles,_isFacingRight);

      //      dig.Dig(_controller.transform.position, aim.GetAngle(), boneFollower.transform.position, _controller.transform.rotation, _isFacingRight);
    }

    //Weaponchange input
    if (Input.GetKeyDown(prevWeapon)) {
      weapon = weaponHolster.prevWeapon();
    }

    if (Input.GetKeyDown(nextWeapon)) {
      weapon = weaponHolster.nextWeapon();
    }

    if (Input.GetKey (shoot) && Time.time > nextFire){
      nextFire = Time.time + weapon.getFireRate();

      if(!isIgnoreFirstShot) {
        weapon.Shoot(boneFollower.transform.position, boneFollower.transform.eulerAngles,_isFacingRight);
      }else{
        isIgnoreFirstShot = false;
      }
      animPlayer.Shoot();
    }

    if (Input.GetKeyUp (shoot)) {
			if(!text4){
				tutorialText.text = "Aim up: W or Up Arrow";
				text4 = true;
			}
      isAimOnce = false;
      isIgnoreFirstShot = true;
    }

    if (Input.GetKey (KeyCode.Escape)) {
      Application.Quit();
    }
  }


  private void Flip() {
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    tempText.transform.localScale = new Vector3(-tempText.transform.localScale.x, tempText.transform.localScale.y, tempText.transform.localScale.z);
    _isFacingRight = transform.localScale.x > 0;
  }
}
