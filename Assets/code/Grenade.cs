﻿using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
  public GameObject Spawn;
  public bool destroy = false;
  public float SpawnCooldown;
  public float damageToGive;

	public void Update() {
		if (destroy) {
			Destroy(gameObject);
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "PlayerSpawn(Clone)")
		{
			other.gameObject.SendMessage("FireBloodParticles",gameObject.transform.position);
			other.gameObject.SendMessage("TakeDamage", damageToGive);
			other.gameObject.GetComponent<Player>().isHitByGrenadeSet(true);
			destroy = true;
			if (Spawn != null) {
				var contact0 = other.contacts [0];
				Instantiate (Spawn, contact0.point, transform.rotation);
			}	
		}
		else
		{
		    destroy = true;
			if (Spawn != null) {
				var contact0 = other.contacts [0];
				Instantiate (Spawn, contact0.point, transform.rotation);
			}
		}
	}	
}