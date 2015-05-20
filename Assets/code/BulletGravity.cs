using UnityEngine;
using System.Collections;
public class BulletGravity : MonoBehaviour {
  public GameObject Spawn;

  public float SpawnCooldown = 1.0F;

  protected virtual void OnCollisionEnter2D(Collision2D collision) {
    Destroy (gameObject);
    if (Spawn != null) {
      var contact0 = collision.contacts [0];
      Instantiate (Spawn, contact0.point, transform.rotation);
    }
  }
}