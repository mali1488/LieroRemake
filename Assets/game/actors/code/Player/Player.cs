﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool _isFacingRight;
	public CharacterController2D _controller;
	private float _normalizedHorizontalSpeed;
	private float _normalizedHorizontalSpeedP2;
	
	public float MaxSpeed = 8f;
	public float SpeedAccelerationOnGround = 10f;
	public float SpeedAccelerationInAir = 5f;
  
	public bool justFlipped = false;
	public bool player1;
	public bool player2;
  	public Aim aim;

 	// input variables player one
	private string moveRightPlayer1;
	private string moveLeftPlayer1;
	private string aimUpPlayer1;
	private string aimDownPlayer1;
	private string prevWeaponPlayer1;
	private string nextWeaponPlayer1;
	private string shootPlayer1;
	private string jump1;
	private string digPlayer1;

	private string moveRightPlayer2;
	private string moveLeftPlayer2;
	private string aimUpPlayer2;
	private string aimDownPlayer2;
	private string shootPlayer2;
	private string jump2;
	private string digPlayer2;

	//Weapon variables
	public ArrayList weaponList = new ArrayList ();
	public int currentWeapon = 0;
	public ChangeWeapon changeWeapon;
	private weapon weapon;

	//digging
	private Digging dig;


	public void Start() {	
		_controller = GetComponent<CharacterController2D>();
    	_isFacingRight = transform.localScale.x > 0;
		//Debug.Log("transform = " + _controller.transform);
		aim.aiming(_isFacingRight, _controller.transform.position);
		for (int i=0; i < 5; i++) {
			weaponList.Add(i);
		}
		weapon = new weapon ();
		dig = new Digging ();


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
		digPlayer1 = ;

		moveRightPlayer2 = PlayerPrefs.GetString ("right2");
		moveLeftPlayer2 = PlayerPrefs.GetString ("left2");
		aimUpPlayer2 = PlayerPrefs.GetString ("up2");
		aimDownPlayer2 = PlayerPrefs.GetString ("down2");
		shootPlayer2 = PlayerPrefs.GetString ("shoot2");
		jump2 = PlayerPrefs.GetString ("jump2");
		digPlayer2 =;
		*/

		// Temporary keybindings for developing purposes
		moveRightPlayer1 = "d";
		moveLeftPlayer1 = "a";
		aimUpPlayer1 = "w";
		aimDownPlayer1 = "s";
		prevWeaponPlayer1 = "e";
		nextWeaponPlayer1 = "1";
		shootPlayer1 = "z";
		jump1 = "space";
		digPlayer1 = "f";

		moveRightPlayer2 = "right";
		moveLeftPlayer2 = "left";
		aimUpPlayer2 = "up";
		aimDownPlayer2 = "down";
		shootPlayer2 = "m";
		jump2 = "right shift";
		digPlayer2 = "n";

	}


	public void Update() {
    	HandleInput();
    	var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
		if (player1) {
			_controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
			aim.aiming (_isFacingRight, _controller.transform.position);
		} else {
			_controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeedP2 * MaxSpeed, Time.deltaTime * movementFactor));
			aim.aiming (_isFacingRight, _controller.transform.position);
		}
	}

	private void HandleInput() {

    	if (Input.GetKey(moveRightPlayer1) && player1) {
    		_normalizedHorizontalSpeed = 2;
			if(!_isFacingRight) {
        		Flip();
				aim.flipAim(_isFacingRight);
      		}
    	} else if (Input.GetKey(moveLeftPlayer1) && player1) {
      		_normalizedHorizontalSpeed = -2;
      		if(_isFacingRight) {
        		Flip();	
				aim.flipAim(_isFacingRight);
      		}
    	} else {
      		_normalizedHorizontalSpeed = 0;
    	}
	
		if (_controller.CanJump && Input.GetKeyDown(jump1) && player1) {
      		_controller.Jump();
    	}
	
		//Aimingkey input
		if (Input.GetKeyDown(aimUpPlayer1) && player1) {
				aim.up = true;
		}

		if (Input.GetKeyUp (aimUpPlayer1) && player1) {
				aim.up = false;	
		}
		
		if (Input.GetKeyDown(aimDownPlayer1) && player1) {
				aim.down = true;
		}

		if (Input.GetKeyUp (aimDownPlayer1) && player1) {
				aim.down = false;
		}

		//Weaponchange input
		if (Input.GetKeyDown(prevWeaponPlayer1) && player1) {
			if (currentWeapon == 0) {
				currentWeapon = changeWeapon.weaponList.Count-1;
			} else {
			    currentWeapon -= 1;
			}
				changeWeapon.swapWeapon(currentWeapon);
		}

		if (Input.GetKeyDown(nextWeaponPlayer1) && player1) {
			if (currentWeapon == changeWeapon.weaponList.Count-1) {
				currentWeapon = 0;
			} else {
				currentWeapon += 1;
			}
				changeWeapon.swapWeapon(currentWeapon);
		}

		if (Input.GetKeyDown (shootPlayer1) && player1 ) {
			weapon.shoot(_controller.transform.position, aim.angle, aim.sight.position,_controller.transform.rotation,_isFacingRight);

	
		}

		if (Input.GetKeyDown (digPlayer1) && player1 ) {
			dig.Dig(_controller.transform.position, aim.angle, aim.sight.position, _controller.transform.rotation, _isFacingRight);
		}

		//player2 handeling
		if (Input.GetKey(moveRightPlayer2) && player2) {
			_normalizedHorizontalSpeedP2 = 2;
			if(!_isFacingRight) {
				Flip();
				aim.flipAim(_isFacingRight);
			}
		} else if (Input.GetKey(moveLeftPlayer2) && player2) {
			_normalizedHorizontalSpeedP2 = -2;
			if(_isFacingRight) {
				Flip();	
				aim.flipAim(_isFacingRight);
			}
		} else {
			_normalizedHorizontalSpeedP2 = 0;
		}

			//Aimingkey input
		if (Input.GetKeyDown(aimUpPlayer2) && player2) {
			aim.up = true;
		}
		
		if (Input.GetKeyUp (aimUpPlayer2) && player2) {
			aim.up = false;	
		}
		
		if (Input.GetKeyDown(aimDownPlayer2) && player2) {
			aim.down = true;
		}
			
		if (Input.GetKeyUp (aimDownPlayer2) && player2) {
			aim.down = false;
		}

		if (Input.GetKeyDown (shootPlayer2) && player2 ) {
			weapon.shoot(_controller.transform.position, aim.angle, aim.sight.position,_controller.transform.rotation,_isFacingRight);
		}

		if (_controller.CanJump && Input.GetKeyDown(jump2) && player2) {
			_controller.Jump();
		}

		if (Input.GetKeyDown (digPlayer2) && player2 ) {
			dig.Dig(_controller.transform.position, aim.angle, aim.sight.position, _controller.transform.rotation, _isFacingRight);
		}
	}

	private void Flip() {
		justFlipped = true;
	    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	    _isFacingRight = transform.localScale.x > 0;
		aim.arrow.localScale = new Vector3(-aim.arrow.localScale.x, aim.arrow.localScale.y, aim.arrow.localScale.z);
		if (aim.aimingBelow && !_isFacingRight) {
			aim.arrow.rotation = Quaternion.AngleAxis (aim.degAngle, Vector3.forward);

		} else  if (aim.aimingBelow && _isFacingRight) {
			aim.arrow.rotation = Quaternion.AngleAxis (-aim.degAngle, Vector3.forward);

		} else if (_isFacingRight) {
			aim.arrow.rotation = Quaternion.AngleAxis (aim.degAngle, Vector3.forward);
		} else {
			aim.arrow.rotation = Quaternion.AngleAxis (-aim.degAngle, Vector3.forward);
		}
	
	}
}
