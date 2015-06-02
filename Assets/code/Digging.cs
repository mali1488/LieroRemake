using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

  public Rigidbody2D digObject;
  private float length = 10.0F;

  public void Dig(Vector3 position, Vector3 rotation, bool isFacingRight) {
    GameObject i = (GameObject)Instantiate (Resources.Load("digObject"),position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
  }
}
