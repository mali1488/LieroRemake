using UnityEngine;
using System.Collections;

public class SplashDamage : MonoBehaviour {
	public float damageToGive;

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
