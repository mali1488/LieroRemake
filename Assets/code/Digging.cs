using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

  public Rigidbody2D digObject;
  private float length = 4.0F;

  public void Dig(Vector3 playerPos, float angle, Vector3 aimPos, Quaternion  playerRotation, bool isFacingRight) {
    GameObject i = (GameObject)Instantiate (Resources.Load("digObject"),
                                            new Vector3(playerPos.x + length*Mathf.Cos (angle),
                                                        playerPos.y + length*Mathf.Sin(angle),
                                                        playerPos.z),
                                            playerRotation);
  }
}
