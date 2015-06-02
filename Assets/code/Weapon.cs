using UnityEngine;
using System.Collections;
using Spine;

public class Weapon {

  private string weaponAttachment;
  private float damage;
  private float speed;
  private float fireRate;
  private GameObject bulletPrefab;
  private GameObject bullet;

  private Bone thompsonBarrel;
  private Bone thompsonAngle;

  private SkeletonAnimation skeletonAnimation = null;

  public Weapon(string weaponAttachment, float damage, float speed, float fireRate, GameObject bulletPrefab) {
    this.weaponAttachment = weaponAttachment;
    this.damage = damage;
    this.speed = speed;
    this.fireRate = fireRate;
    this.bulletPrefab = bulletPrefab;
  }

  public void Shoot(Vector3 position, Vector3 rotation, bool isFacingRight) {
    if (isFacingRight) {
      bullet = GameObject.Instantiate(bulletPrefab, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z)) as GameObject;
      bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * speed);
    }else{
      bullet = GameObject.Instantiate(bulletPrefab, position, Quaternion.Euler(rotation.x, rotation.y, -rotation.z)) as GameObject;
      bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.right * speed);
    }
  }

  public float getFireRate() {
    return this.fireRate;
  }

  public string getAttachment() {
    Debug.Log("Weapon.getAttachment: " + weaponAttachment);
    return this.weaponAttachment;
  }
}