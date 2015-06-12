using UnityEngine;
using System.Collections;

public class SplashDamage : MonoBehaviour {
	public float damageToGive;

	//Handles the explosion damage from grenades or rockets.
	//If the player was directly hit by the grenade/rocket, it should not take
	//any additional damage, but if it was just caught in the explosion, damage will be dealt

	protected virtual void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name == "PlayerSpawn(Clone)")
		{
			if(!other.gameObject.GetComponent<Player>().isHitByGrenadeGet())
			{
				other.gameObject.SendMessage("FireBloodParticles",gameObject.transform.position);
				other.gameObject.SendMessage("TakeDamage", damageToGive);
			}
			other.gameObject.GetComponent<Player>().isHitByGrenadeSet(false);
			
		}
	}
}
