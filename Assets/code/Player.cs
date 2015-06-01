using UnityEngine;
using System.Collections;
using Spine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {

  public AudioClip audioShoot;
  public AudioClip[] audioDie;

  AudioSource audio;

  public Image healthbar;
  private float maxHealth = 100;
  private float minHealth = 0;
  private float curHealth;
  private float healthSpeed;


  public GameObject boneFollower;
  public bool _isFacingRight;
  private CharacterController2D _controller;
  private float _normalizedHorizontalSpeed;

  private float nextFire = 0.0F;
  private bool isAimOnce = false;
  private bool isIgnoreFirstShot = true;

  private Vector2 transformBackUp;

  public float MaxSpeed = 8f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;

  private GameManager manager;
  public bool IsDead {get; private set;}

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
    transformBackUp = transform.position;
    tempCam.height = camHeight;
    tempCam.width = camWidth;
    tempCam.y = camY;
    tempCam.x = camX;
    GameObject tempChild = this.transform.GetChild (1).gameObject;
    tempChild.GetComponent<Camera> ().rect = tempCam;
  }

  public void Start() {
    _controller = GetComponent<CharacterController2D>();
    dig = GetComponent<Digging>();
    _isFacingRight = transform.localScale.x > 0;

    curHealth = maxHealth;
    healthSpeed = 10f;

    audio = GetComponent<AudioSource>();

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

  public void setGameManager(GameManager managerToUse)
  {
    manager = managerToUse;
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
      dig.Dig(_controller.transform.position, aim.GetAngle(), boneFollower.transform.position, _controller.transform.rotation, _isFacingRight);
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
      Debug.Log("Nextfire" + weapon.getFireRate());


      if(!isIgnoreFirstShot) {
        weapon.Shoot(boneFollower.transform.position, boneFollower.transform.eulerAngles,_isFacingRight);
      }else{
        isIgnoreFirstShot = false;
      }

      audio.PlayOneShot(audioShoot, 0.7F);

      animPlayer.Shoot();
    }

    if (Input.GetKeyUp (shoot)) {
      isAimOnce = false;
      isIgnoreFirstShot = true;
    }

    if (Input.GetKeyDown ("u") && !IsDead) {
      TakeDamage(10f);
    }

  }
  private void TakeDamage(float adj) {
		curHealth = Mathf.Clamp (curHealth -= adj, 0, maxHealth);
	
		healthbar.fillAmount = curHealth / maxHealth;

		if (curHealth > maxHealth / 2) {
			healthbar.color = new Color32 ((byte)Map (curHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
		} else {
			healthbar.color = new Color32 (255, (byte)Map (curHealth, 0, maxHealth / 2, 0, 255), 0, 255);
		}
		if (curHealth <= 0.0)
			manager.KillPlayer (this);
		Debug.Log ("Nu har jag " + curHealth + " kvar");
	}
  }

  private float Map(float x, float inMin, float inMax, float outMin, float outMax) {
    return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	
  private void Flip() {
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    _isFacingRight = transform.localScale.x > 0;
  }

  public void Kill()
  {
    _controller.HandleCollisions = false;
    GetComponent<Collider2D>().enabled = false;
    IsDead = true;
    curHealth = 0f;
    _controller.SetForce(new Vector2(0,10));
    if (audio.isPlaying) return;
    audio.clip = audioDie[UnityEngine.Random.Range(0,2)];
    audio.Play();
  }

  public void RespawnAt()
  {
    if(!_isFacingRight)
      Flip();
    IsDead = false;
    GetComponent<Collider2D>().enabled = true;
    _controller.HandleCollisions = true;
    curHealth = maxHealth;
	healthbar.fillAmount = 1;
	healthbar.color = new Color32 ((byte)Map (curHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
	
    transform.position = transformBackUp;
  }
}
