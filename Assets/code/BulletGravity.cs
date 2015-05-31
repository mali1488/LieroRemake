﻿using UnityEngine;
using System.Collections;

public class BulletGravity : MonoBehaviour {
  public GameObject Spawn;
  public int damageToGive;

  public float SpawnCooldown = 1.0F;

  protected virtual void OnCollisionEnter2D(Collision2D other) {
  if(other.gameObject.name == "PlayerSpawn(Clone)")
    {
      Debug.Log(other.gameObject.name);
      //other.gameObject.SendMessage("FireBloodParticles",gameObject.transform.position, gameObject.transform.position.x > other.gameObject.transform.position.x);
      other.gameObject.SendMessage("FireBloodParticles",gameObject.transform.position);
      other.gameObject.SendMessage("TakeDamage",5);
      Destroy(gameObject);

      //Debug.Log("CharacterController2D.OnTriggerEnter2D: Aj!");
      // Destroy(gameObject);
    }
    else
      Debug.Log(gameObject.name);
      Destroy(gameObject);
      
  }
    
}