using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  public bool _isFacingRight;
  public CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;
  private float _normalizedHorizontalSpeedP2;
	
  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;
  
  
  public Aim aim;
  public bool justFlipped = false;
  public bool player1;
  public bool player2;
  
  // keycode variables p1
  public KeyCode moveRightPlayer1 = KeyCode.D;
  public KeyCode moveLeftPlayer1 = KeyCode.A;
  public KeyCode aimUpPlayer1 = KeyCode.W;
  public KeyCode aimDownPlayer1 = KeyCode.S;
  private KeyCode prevWeaponPlayer1 = KeyCode.Q;
  private KeyCode nextWeaponPlayer1 = KeyCode.E;
  private KeyCode shootPlayer1 = KeyCode.Z;

  //keycodes p2
  private KeyCode moveRightPlayer2 = KeyCode.RightArrow;
  private KeyCode moveLeftPlayer2 = KeyCode.LeftArrow;
  private KeyCode aimUpPlayer2 = KeyCode.UpArrow;
  private KeyCode aimDownPlayer2 = KeyCode.DownArrow;
  private KeyCode shootPlayer2 = KeyCode.LeftControl;
  //Weapon variables
  public ArrayList weaponList = new ArrayList ();
  public int currentWeapon = 0;
  public ChangeWeapon changeWeapon;
  private weapon weapon;

  public void Start() {
    _controller = GetComponent<CharacterController2D>();
    _isFacingRight = transform.localScale.x > 0;
	//Debug.Log("transform = " + _controller.transform);
	aim.aiming(_isFacingRight, _controller.transform.position);
	for (int i=0; i < 5; i++) {
		weaponList.Add(i);
	}
	weapon = new weapon ();
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
      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
		aim.flipAim(_isFacingRight);
      }
    } else if (Input.GetKey(moveLeftPlayer1) && player1) {
      _normalizedHorizontalSpeed = -1;
      if(_isFacingRight) {
        Flip();	
		aim.flipAim(_isFacingRight);
      }
    } else {
      _normalizedHorizontalSpeed = 0;
    }

	
		
	if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space) && player1) {
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

	//player2 handeling
	if (Input.GetKey(moveRightPlayer2) && player2) {
		_normalizedHorizontalSpeedP2 = 1;
		if(!_isFacingRight) {
			Flip();
			aim.flipAim(_isFacingRight);
		}
	} else if (Input.GetKey(moveLeftPlayer2) && player2) {
		_normalizedHorizontalSpeedP2 = -1;
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
