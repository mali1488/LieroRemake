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
    tutorialText.text = "Welcome to the Tutorial!";

    tempText.transform.position = new Vector3(charPos.x, charPos.y+100f,charPos.z);

  }

  private void HandleInput() {

    if (Input.GetKey(moveRight)) {

      _normalizedHorizontalSpeed = 1;
      if(!_isFacingRight) {
        Flip();
      }
      if (_controller.State.IsGrounded){
        animPlayer.Move();
      }

      //MOVE LEFT
    } else if (Input.GetKey(moveLeft)) {
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
