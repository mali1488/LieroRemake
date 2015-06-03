using UnityEngine;
using System.Collections;
using Spine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {

  public int lives = 3;
  public int kills = 0;

  public AudioClip audioShoot;
  public AudioClip audioShootBazooka;
  public AudioClip[] audioDie;

  AudioSource audio;

  public Image healthbar;
  private float maxHealth = 100;
  private float minHealth = 0;
  private float curHealth;
  private float healthSpeed;


  public GameObject boneBarrel;
  public GameObject boneBarrelBazooka;
  public GameObject boneCrosshair;
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

  public bool IsDead {get; private set;}
  private bool isHitByGrenade = false;

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
  private string weaponName;
  private bool isBazooka = false;


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
        isBazooka = weapon.IsBazooka();
        SetWeaponName();
      }
      animPlayer = GetComponent<AnimPlayer>();
      if (animPlayer) {
        animPlayer.Setup(skeletonAnimation);
        animPlayer.SetBazooka(isBazooka);
      }
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
      if (_controller.State.IsGrounded && !Input.GetKey(shoot) && !IsDead) {
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
      dig.Dig(boneCrosshair.transform.position, boneCrosshair.transform.eulerAngles,_isFacingRight);
      //dig.Dig(_controller.transform.position, boneBarrel.transform.rotation, boneBarrel.transform.position, _controller.transform.rotation, _isFacingRight);
    }

    //Weaponchange input
    if (Input.GetKeyDown(prevWeapon)) {
      weapon = weaponHolster.prevWeapon();
      isBazooka = weapon.IsBazooka();
      SetWeaponName();
      animPlayer.SetBazooka(isBazooka);
    }

    if (Input.GetKeyDown(nextWeapon)) {
      weapon = weaponHolster.nextWeapon();
      SetWeaponName();
      isBazooka = weapon.IsBazooka();
      animPlayer.SetBazooka(isBazooka);
    }

    if (Input.GetKey (shoot) && Time.time > nextFire){
      nextFire = Time.time + weapon.getFireRate();

      if(isBazooka) {
        weapon.Shoot(boneBarrelBazooka.transform.position, boneBarrelBazooka.transform.eulerAngles,_isFacingRight);
        audio.PlayOneShot(audioShootBazooka, 0.7F);
      }else{
        audio.PlayOneShot(audioShoot, 0.7F);
        weapon.Shoot(boneBarrel.transform.position, boneBarrel.transform.eulerAngles,_isFacingRight);
      }

      animPlayer.Shoot();
    }

    if (Input.GetKeyUp (shoot)) {
      isAimOnce = false;
      isIgnoreFirstShot = true;
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

  }

  private float Map(float x, float inMin, float inMax, float outMin, float outMax) {
    return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
  }

  private void Flip() {
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    _isFacingRight = transform.localScale.x > 0;
  }

  public void Die()
  {
    _controller.HandleCollisions = false;
    IsDead = true;
    animPlayer.FallForward ();
    curHealth = 0f;
    if(lives <= 0) {
      lives = 0;
    }else{
      lives--;
    }
    if (audio.isPlaying) return;
    audio.clip = audioDie[UnityEngine.Random.Range(0,2)];
    audio.Play();

  }

  public void RespawnAt()
  {
    StartCoroutine(WaitSpawn());
  }

  IEnumerator WaitSpawn() {
    yield return new WaitForSeconds(4);
    if(!_isFacingRight)
      Flip();
    IsDead = false;
    GetComponent<Collider2D>().enabled = true;
    _controller.HandleCollisions = true;
    curHealth = maxHealth;
    healthbar.fillAmount = 1;
    healthbar.color = new Color32 ((byte)Map (curHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
    animPlayer.Idle ();
    transform.position = transformBackUp;
    Debug.Log("Respawn: " + transform.position);

  }

  public void isHitByGrenadeSet(bool state)
  {
    isHitByGrenade = state;
  }

  public bool isHitByGrenadeGet()
  {
    return isHitByGrenade;
  }

  public int GetLives() {
    return lives;
  }

  public int GetKills() {
    return kills;
  }

  public string GetWeaponName() {
    return weaponName;
  }

  public float GetHealth() {
    return curHealth;
  }

  public void SetKills() {
    kills++;
  }

  private void SetWeaponName() {
    weaponName = weapon.getAttachment();
  }
}
