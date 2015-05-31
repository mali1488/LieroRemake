using UnityEngine;
using System.Collections;

public class KillParticleEffect : MonoBehaviour
{
  void Update () {
    if(!GetComponent<ParticleSystem>().IsAlive()) Destroy(gameObject);
  }
}