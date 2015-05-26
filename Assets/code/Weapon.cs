using UnityEngine;
using System.Collections;
using Spine;

public class Weapon : MonoBehaviour {

  public GameObject bulletPrefab;
  private GameObject bullet;
  private float damage;

  private Bone thompsonBarrel;
  private Bone thompsonAngle;

  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
    thompsonBarrel = this.skeletonAnimation.skeleton.FindBone("thompsonBarrel");
  }

  public void Shoot(Vector3 position, Vector3 rotation, bool isFacingRight) {
    float speed = 5000.0F;

    if (isFacingRight) {
      bullet = (GameObject)Instantiate(bulletPrefab, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
      bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * speed);
    }else{
      bullet = (GameObject)Instantiate(bulletPrefab, position, Quaternion.Euler(rotation.x, rotation.y, -rotation.z));
      bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.right * speed);
    }
  }

}