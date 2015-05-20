using UnityEngine;
using System.Collections;
using Spine;

public class Bullet : MonoBehaviour {

  public void Setup(float x, float y, float speed) {
    GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * speed, ForceMode2D.Impulse);
  }
}