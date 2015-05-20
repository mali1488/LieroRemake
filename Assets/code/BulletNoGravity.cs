using UnityEngine;
using System.Collections;

public class BulletNoGravity : MonoBehaviour {
        public Rigidbody2D r;
        public float bulletAngle;
        public float bulletSpeed;

        public void set(float angle,bool isFacingRight,float speed){
                bulletSpeed = speed;
                bulletAngle = angle;
                //Debug.Log ("set");
        }
        // Update is called once per frame
        void Update () {
                Rigidbody2D r = GetComponent<Rigidbody2D> ();
                r.velocity = new Vector2 (Mathf.Cos(bulletAngle) * bulletSpeed, Mathf.Sin (bulletAngle) * bulletSpeed);
        }
}
