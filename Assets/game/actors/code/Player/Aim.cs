using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

    
	private float offset = 4.0f;
	private float sightOffset = 20.0f;
    //Arrow vars
	public Transform arrow;
	public Transform sight;
	public float angle;
	public bool up = false;
	public bool down = false;
	public bool aimingBelow;
	public float degAngle;
	public Player player;

	void Start () {
		angle = 0.0f;
	}

	public void aiming (bool facingRight, Vector3 playerPosition){
		
		aimingBelow = playerPosition.y > sight.transform.position.y;

		if (up) {
			if (facingRight){
				moveAimUp(facingRight, playerPosition);
			} else {
				moveAimDown(facingRight, playerPosition);

			}
		} else if(down){
			if(facingRight){
				moveAimDown(facingRight, playerPosition);
			} else{
				moveAimUp(facingRight, playerPosition);
			} 
		}
		
		if (!up && !down) {
			//Debug.Log ("specialcase");

			//måste fixas för att arrow ska vändas rätt.. 
		}
		arrow.position = new Vector3 (playerPosition.x + offset*Mathf.Cos(angle), playerPosition.y + offset*Mathf.Sin(angle), playerPosition.z);
		sight.position = new Vector3 (playerPosition.x + sightOffset*Mathf.Cos(angle), 
		                              playerPosition.y + sightOffset*Mathf.Sin(angle), playerPosition.z);
	}

	public void moveAimUp(bool facingRight, Vector3 playerPosition) {
		degAngle = 180.0f * Mathf.Acos (Mathf.Cos (angle)) / Mathf.PI; 
		aimingBelow = playerPosition.y > sight.transform.position.y; 

		if (degAngle >= 90.0f && facingRight) {
			if(aimingBelow){
				
				Debug.Log("3");
				angle +=0.03f;
			} else { 
				angle = Mathf.PI / 2.0f;
			}
			arrow.rotation = Quaternion.AngleAxis(degAngle, Vector3.forward);
		} else if (degAngle<= 90.0f && facingRight) {
			angle += 0.03f;
			if(aimingBelow){
				arrow.rotation = Quaternion.AngleAxis(-degAngle, Vector3.forward);
			} else {
				arrow.rotation = Quaternion.AngleAxis(degAngle, Vector3.forward);
			}

		} else if (degAngle >= 90.0f && !facingRight) {
			angle += 0.03f;

			if(aimingBelow){
				arrow.rotation = Quaternion.AngleAxis(degAngle, Vector3.forward);
			} else {
				arrow.rotation = Quaternion.AngleAxis(-degAngle, Vector3.forward);
			}
		}
	}

	public void moveAimDown(bool facingRight, Vector3 playerPosition) {
		degAngle = 180.0f * Mathf.Acos (Mathf.Cos (angle)) / Mathf.PI;
		aimingBelow = playerPosition.y > sight.transform.position.y; 

		if (degAngle <= 90.0f && facingRight) {
			angle -= 0.03f;

			if(aimingBelow){
				arrow.rotation = Quaternion.AngleAxis(-degAngle, Vector3.forward);
			} else {

				arrow.rotation = Quaternion.AngleAxis(degAngle , Vector3.forward);
			}
		} else if (degAngle <= 90.0f && !facingRight) {
			if(aimingBelow){
				angle -= 0.03f;
			} else {
				angle = Mathf.PI / 2.0f;
			}
			arrow.rotation = Quaternion.AngleAxis(degAngle -180.0f, Vector3.forward);
		} else if (degAngle >= 90.0f && !facingRight) {
			angle -= 0.03f;

			if(aimingBelow){
				arrow.rotation = Quaternion.AngleAxis(degAngle, Vector3.forward);
			} else {
				arrow.rotation = Quaternion.AngleAxis(-degAngle, Vector3.forward);
			}
		}
	}

	
	public void flipAim(bool facing){

		if (facing == false) {
			angle -= Mathf.PI;
			angle *= -1;
		} else if(facing){
			angle = Mathf.PI - angle;
		}
		
	}

	// Update is called once per frame
	void Update () {

	}
}
