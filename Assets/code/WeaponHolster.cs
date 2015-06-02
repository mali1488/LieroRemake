using UnityEngine;
using System.Collections;
using Spine;

public class WeaponHolster : MonoBehaviour {

  private SkeletonAnimation skeletonAnimation;

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
  public GameObject rocketPrefab;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
    carbine = new Weapon(skeletonAnimation, "carbine", 20f, 5000f, 0.15f, bulletPrefab);
    flameGun = new Weapon(skeletonAnimation, "flamegun", 20f, 5000f, 0.15f, bulletPrefab);
    mp40 = new Weapon(skeletonAnimation, "thompson", 20f, 5000f, 0.15f, bulletPrefab);
    thompson = new Weapon(skeletonAnimation, "mp40", 20f, 5000f, 0.15f, bulletPrefab);
    bazooka = new Weapon(skeletonAnimation, "bazooka", 40f, 7000f, 1f, rocketPrefab);

    weaponList.Add(carbine);
    weaponList.Add(flameGun);
    weaponList.Add(mp40);
    weaponList.Add(thompson);
    weaponList.Add(bazooka);
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
    if(currentWeapon <= 0) {
      currentWeapon = weaponList.Count-1;
    }else{
      currentWeapon--;
    }
    return getCurrentWeapon();
  }

  public Weapon getCurrentWeapon() {
    Weapon weapon = (Weapon)weaponList[currentWeapon];
    this.skeletonAnimation.skeleton.SetAttachment("weapon", weapon.getAttachment());
    foreach(object o in weaponList)
    {
      Weapon newWeapon = (Weapon)o;
      if(weaponList[currentWeapon] == newWeapon) {
        newWeapon.ShowCrossHair();
      }else{
        newWeapon.HideCrossHair();
      }
    }

    return weapon;
  }

  public void SetWeapon(int newWeapon) {
    if(newWeapon == currentWeapon) {
      return;
    }

    currentWeapon = newWeapon;
  }

}