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
  public Aim aim;

  //Input variables player
  private string moveRight;
  private string moveLeft;
  private string aimUp;
  private string aimDown;
  private string prevWeapon;
  private string nextWeapon;
  private string shoot;
  private string jump;
  private string digging;

  //Weapon variables
  public ArrayList weaponList = new ArrayList ();
  public int currentWeapon = 0;
  public ChangeWeapon changeWeapon;
  private weapon weapon;

	
  //digging
  private Digging dig;

  //camera
  private Camera cam;

  //Spine animation

  [SpineAnimation("idle")]
  public string idleAnimation;

  [SpineAnimation("run")]
  public string moveAnimation;

  [SpineAnimation("attack")]
  public string attackAnimation;

  [SpineAnimation("jump")]
  public string jumpAnimation;

  [SpineSlot]
  public string gunSlot;

  [SpineAttachment(currentSkinOnly: true, slotField: "gunSlot")]
  public string carbineAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "gunSlot")]
  public string mp40Attachment;

  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(String moveLeft, String moveRight, String aimUp, String aimDown, String prevWeapon, String nextWeapon, String shoot, String jump, String digging, int positionX, int positionY) {
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
  }

  public void Start() {
    // Get the SkeletonAnimation component for the GameObject this script is attached to.
    skeletonAnimation = GetComponent<SkeletonAnimation>();

    skeletonAnimation.skeleton.SetAttachment(gunSlot, carbineAttachment);

    _controller = GetComponent<CharacterController2D>();
    _isFacingRight = transform.localScale.x > 0;
    //Debug.Log("transform = " + _controller.transform);
    aim.aiming(_isFacingRight, _controller.transform.position);
    for (int i=0; i < 5; i++) {
      weaponList.Add(i);
    }
    weapon = new weapon ();
	dig = new Digging  ();
	cam = GetComponent<Camera> ();
  }

  public void Update() {
    HandleInput();
    var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
      _controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
      aim.aiming (_isFacingRight, _controller.transform.position);
  }

  private void HandleInput() {

    //MOVE RIGHT
    if (Input.GetKey(moveRight)) {

      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
        aim.flipAim(_isFacingRight);
      }
      if (_controller.State.IsGrounded){
        skeletonAnimation.AnimationName = moveAnimation;
      }

    //MOVE LEFT
    } else if (Input.GetKey(moveLeft)) {
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
    if (Input.GetKeyDown(aimUp)) {
      aim.up = true;
    }

    if (Input.GetKeyUp (aimUp)) {
      aim.up = false;
    }
    if (Input.GetKeyDown(aimDown)) {
      aim.down = true;
    }

    if (Input.GetKeyUp (aimDown)) {
      aim.down = false;
    }
	
	if (Input.GetKeyDown (digging)) {
		dig.Dig(_controller.transform.position, aim.angle, aim.sight.position, _controller.transform.rotation, _isFacingRight);
	}

    //Weaponchange input
    if (Input.GetKeyDown(prevWeapon)) {
      if (currentWeapon == 0) {
        currentWeapon = changeWeapon.weaponList.Count-1;
      } else {
        currentWeapon -= 1;
      }
      changeWeapon.swapWeapon(currentWeapon);
    }

    if (Input.GetKeyDown(nextWeapon)) {
      if (currentWeapon == changeWeapon.weaponList.Count-1) {
        currentWeapon = 0;
      } else {
        currentWeapon += 1;
      }
      changeWeapon.swapWeapon(currentWeapon);
    }

    if (Input.GetKeyDown (shoot) ){
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
