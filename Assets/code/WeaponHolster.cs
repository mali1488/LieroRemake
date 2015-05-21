using UnityEngine;
using System.Collections;
using Spine;

public class WeaponHolster : MonoBehaviour {

  private SkeletonAnimation skeletonAnimation;

  [SpineSlot]
  public string weaponSlot;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string bazookaAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string carbineAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string colt45Attachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string flameGunAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string mp40Attachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string potatoMasherAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string rifleAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "weaponSlot")]
  public string thompsonAttachment;

  private Weapon bazooka;
  private Weapon carbine;
  private Weapon colt45;
  private Weapon flameGun;
  private Weapon mp40;
  private Weapon potatoMasher;
  private Weapon rifle;
  private Weapon thompson;

  private int currentWeapon = 0;

  private ArrayList weaponList = new ArrayList();

  public GameObject bulletPrefab;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
    carbine = new Weapon();
    flameGun = new Weapon();
    mp40 = new Weapon();
    thompson = new Weapon();
    carbine.Setup(carbineAttachment, 5f, 5000f, 0.35f, bulletPrefab);
    flameGun.Setup(flameGunAttachment, 5f, 5000f, 0.35f, bulletPrefab);
    mp40.Setup(mp40Attachment, 5f, 5000f, 0.35f, bulletPrefab);
    thompson.Setup(thompsonAttachment, 5f, 5000f, 0.35f, bulletPrefab);

    weaponList.Add(carbine);
    weaponList.Add(flameGun);
    weaponList.Add(mp40);
    weaponList.Add(thompson);

  }
  public Weapon nextWeapon() {
    if(weaponList.Count-1 == currentWeapon) {
      currentWeapon = 0;
    }else{
      currentWeapon++;
    }
    return getCurrentWeapon();
  }

  public Weapon prevWeapon() {
    if(currentWeapon == 0) {
      currentWeapon = weaponList.Count-1;
    }else{
      currentWeapon--;
    }
    return getCurrentWeapon();
  }

  public Weapon getCurrentWeapon() {
    Debug.Log("WeaponHolster.getCurrentWeapon(): " + currentWeapon);
    Weapon weapon = (Weapon)weaponList[currentWeapon];
    skeletonAnimation.skeleton.SetAttachment(weaponSlot, weapon.getAttachment());
    return weapon;
  }

}