using UnityEngine;
using System.Collections;
using Spine;

public class Weapon {

  private string weapon;
  private float damage;
  private float speed;
  private float fireRate;
  private GameObject bulletPrefab;
  private GameObject bullet;

  public void Setup(string weapon, float damage, float speed, float nextFire, GameObject bulletPrefab) {
    this.weapon = weapon;
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
    return this.weapon;
  }
}