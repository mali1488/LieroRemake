using UnityEngine;
using System.Collections;
using Spine;
using System;

public class Player : MonoBehaviour {

  public GameObject boneFollower;
  public bool _isFacingRight;
  private CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;

  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;

  public bool justFlipped = false;
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
  public ArrayList weaponList = new ArrayList ();
  public int currentWeapon = 0;
  public ChangeWeapon changeWeapon;
  private Weapon weapon;


  //digging
  private Digging dig;

  //camera
  private Camera cam;
  private Rect tempCam;

  //Spine animation

  private AnimPlayer animPlayer = null;
  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(String moveLeft, String moveRight, String aimUp, String aimDown, String prevWeapon, String nextWeapon, String shoot, String jump, String digging, int positionX, int positionY, float camX, float camY, float camWidth, float camHeight) {

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
    GameObject tempChild = this.transform.GetChild (0).gameObject;
        tempChild.GetComponent<Camera> ().rect = tempCam;
}

  public void Start() {
    _controller = GetComponent<CharacterController2D>();
    dig = GetComponent<Digging>();
    _isFacingRight = transform.localScale.x > 0;

    skeletonAnimation = GetComponent<SkeletonAnimation>();

    if (skeletonAnimation) {
      aim = GetComponent<Aim>();
      if (aim) {
        aim.Setup(skeletonAnimation);
      }
      weapon = GetComponent<Weapon>();
      if (weapon) {
        weapon.Setup(skeletonAnimation);
      }
      animPlayer = GetComponent<AnimPlayer>();
      if (animPlayer) {
        animPlayer.Setup(skeletonAnimation);
      }
    }

    //Debug.Log("transform = " + _controller.transform);
    //aim.aiming(_isFacingRight, _controller.transform.position);
    for (int i=0; i < 5; i++) {
      weaponList.Add(i);
    }
  }

  public void Update() {
    HandleInput();
    var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
    _controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
    //aim.aiming (_isFacingRight, _controller.transform.position);
  }

  private void HandleInput() {

    if (Input.GetKey(moveRight)) {

      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
        aim.flipAim(_isFacingRight);
      }
      if (_controller.State.IsGrounded){
        animPlayer.Move();
      }

      //MOVE LEFT
    } else if (Input.GetKey(moveLeft)) {
      _normalizedHorizontalSpeed = -1;
      if(_isFacingRight) {
        Flip();
        aim.flipAim(_isFacingRight);
      }
      if (_controller.State.IsGrounded) {
        animPlayer.Move();
      }
      //IDLE
    } else {
      _normalizedHorizontalSpeed = 0;
      if (_controller.State.IsGrounded) {
        animPlayer.Idle();
      }

    }
    if (_controller.CanJump && Input.GetKeyDown(jump)) {
      _controller.Jump();
      animPlayer.Jump();
    }

    if (Input.GetKey(aimUp)) {
      aim.Up();
    }

    if (Input.GetKey(aimDown)) {
      aim.Down();
    }

        if (Input.GetKey(digging)) {
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
      weapon.Shoot(boneFollower.transform.position, boneFollower.transform.eulerAngles,_isFacingRight);
    }
  }


  private void Flip() {
    justFlipped = true;
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    _isFacingRight = transform.localScale.x > 0;

    /*
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
    */
  }
}
