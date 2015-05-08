using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  public bool _isFacingRight;
  public CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;

  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;
  
  
  public Aim aim;

  // keycode variables
  public KeyCode moveRightPlayer1 = KeyCode.D;
  public KeyCode moveLeftPlayer1 = KeyCode.A;
  public KeyCode aimUpPlayer1 = KeyCode.W;
  public KeyCode aimDownPlayer1 = KeyCode.S;
  private KeyCode prevWeaponPlayer1 = KeyCode.Q;
  private KeyCode nextWeaponPlayer1 = KeyCode.E;

  //Weapon variables
  public ArrayList weaponList = new ArrayList ();
  public int currentWeapon = 0;
  public ChangeWeapon changeWeapon;

  public void Start() {
    _controller = GetComponent<CharacterController2D>();
    _isFacingRight = transform.localScale.x > 0;
	Debug.Log("transform = " + _controller.transform);
	aim.aiming(_isFacingRight, _controller.transform.position);
	for (int i=0; i < 5; i++) {
		weaponList.Add(i);
	}
  }

  public void Update() {
    HandleInput();
    var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
    _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
		aim.aiming(_isFacingRight, _controller.transform.position);

	}

  private void HandleInput() {
    if (Input.GetKey(moveRightPlayer1)) {
      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
		aim.flipAim(_isFacingRight);
      }
    } else if (Input.GetKey(moveLeftPlayer1)) {
            _normalizedHorizontalSpeed = -1;
      if(_isFacingRight) {
        Flip();	
		aim.flipAim(_isFacingRight);
      }
    } else {
      _normalizedHorizontalSpeed = 0;
    }

    if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space)) {
      _controller.Jump();
    }
	
	//Aimingkey input
	if (Input.GetKeyDown(aimUpPlayer1)) {
			aim.up = true;
	}

	if (Input.GetKeyUp (aimUpPlayer1)) {
			aim.up = false;	
	}
	
	if (Input.GetKeyDown(aimDownPlayer1)) {
			aim.down = true;
	}

	if (Input.GetKeyUp (aimDownPlayer1)) {
			aim.down = false;
	}

	//Weaponchange input
	if (Input.GetKeyDown(prevWeaponPlayer1)) {
		if (currentWeapon == 0) {
			currentWeapon = changeWeapon.weaponList.Count-1;
		} else {
		    currentWeapon -= 1;
		}
			changeWeapon.swapWeapon(currentWeapon);
	}

	if (Input.GetKeyDown(nextWeaponPlayer1)) {
		if (currentWeapon == changeWeapon.weaponList.Count-1) {
			currentWeapon = 0;
		} else {
			currentWeapon += 1;
		}
			changeWeapon.swapWeapon(currentWeapon);
	}

  }

  private void Flip() {
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
