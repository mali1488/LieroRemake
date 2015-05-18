using UnityEngine;
using System.Collections;
using Spine;
using System;

public class Player : MonoBehaviour {
  public bool _isFacingRight;
  public CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;

  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;

  public bool justFlipped = false;
  public bool player1;
  public bool player2;
  public Aim aim;

  // input variables player one
  public string moveRightPlayer;
  public string moveLeftPlayer;
  public string aimUpPlayer;
  public string aimDownPlayer;
  public string prevWeaponPlayer;
  public string nextWeaponPlayer;
  public string shootPlayer;
  public string jump;

  //Weapon variables
  public ArrayList weaponList = new ArrayList ();
  public int currentWeapon = 0;
  public ChangeWeapon changeWeapon;
  private weapon weapon;

  //Spine animation

  [SpineAnimation("idle")]
  public string idleAnimation;

  [SpineAnimation("run")]
  public string moveAnimation;

  [SpineAnimation("attack")]
  public string attackAnimation;

  [SpineAnimation("jump")]
  public string jumpAnimation;

  private SkeletonAnimation skeletonAnimation = null;

  public void Start() {
    // Get the SkeletonAnimation component for the GameObject this script is attached to.
    skeletonAnimation = GetComponent<SkeletonAnimation>();

    _controller = GetComponent<CharacterController2D>();
    _isFacingRight = transform.localScale.x > 0;
    //Debug.Log("transform = " + _controller.transform);
    aim.aiming(_isFacingRight, _controller.transform.position);
    for (int i=0; i < 5; i++) {
      weaponList.Add(i);
    }
    weapon = new weapon ();

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
  }

  public void Update() {
    HandleInput();
    var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
      _controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
      aim.aiming (_isFacingRight, _controller.transform.position);
  }

  private void HandleInput() {

    //MOVE RIGHT
    if (Input.GetKey(moveRightPlayer)) {

      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
        aim.flipAim(_isFacingRight);
      }
      if (_controller.State.IsGrounded){
        skeletonAnimation.AnimationName = moveAnimation;
      }

    //MOVE LEFT
    } else if (Input.GetKey(moveLeftPlayer)) {
      _normalizedHorizontalSpeed = -1;
      if(_isFacingRight) {
        Flip();
        aim.flipAim(_isFacingRight);
      }
      if (_controller.State.IsGrounded) {
        skeletonAnimation.AnimationName = moveAnimation;
      }
    //IDLE
    } else {
      _normalizedHorizontalSpeed = 0;
            if (_controller.State.IsGrounded) {
        skeletonAnimation.AnimationName = idleAnimation;
      }

    }
    if (_controller.CanJump && Input.GetKeyDown(jump)) {
      skeletonAnimation.AnimationName = jumpAnimation;
      _controller.Jump();
    }
    //Aimingkey input
    if (Input.GetKeyDown(aimUpPlayer)) {
      aim.up = true;
    }

    if (Input.GetKeyUp (aimUpPlayer)) {
      aim.up = false;
    }
    if (Input.GetKeyDown(aimDownPlayer)) {
      aim.down = true;
    }

    if (Input.GetKeyUp (aimDownPlayer)) {
      aim.down = false;
    }

    //Weaponchange input
    if (Input.GetKeyDown(prevWeaponPlayer)) {
      if (currentWeapon == 0) {
        currentWeapon = changeWeapon.weaponList.Count-1;
      } else {
        currentWeapon -= 1;
      }
      changeWeapon.swapWeapon(currentWeapon);
    }

    if (Input.GetKeyDown(nextWeaponPlayer)) {
      if (currentWeapon == changeWeapon.weaponList.Count-1) {
        currentWeapon = 0;
      } else {
        currentWeapon += 1;
      }
      changeWeapon.swapWeapon(currentWeapon);
    }

    if (Input.GetKeyDown (shootPlayer) ){
      weapon.shoot(_controller.transform.position, aim.angle, aim.sight.position,_controller.transform.rotation,_isFacingRight);
      //skeletonAnimation.AnimationName = attackAnimation;
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
