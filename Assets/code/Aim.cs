using UnityEngine;
using System.Collections;
using Spine;

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

  private Bone rightUpperArm;
  private Bone torso;
  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
    rightUpperArm = this.skeletonAnimation.skeleton.FindBone("rightUpperArm");
    torso = this.skeletonAnimation.skeleton.FindBone("torso");
  }

  public float GetAngle() {
    return rightUpperArm.Rotation;
  }

  public void Shoot() {
    skeletonAnimation.UpdateLocal += delegate(SkeletonRenderer skeletonRenderer) {
    const float lowerRotationBound = 95.0f;
    const float upperArmRotationBound = 200.0f;
    const float upperTorsoRotationBound = 125.0f;

    Debug.Log("Aim.shoot()");
    float tempArmRot = rightUpperArm.Rotation += 20;
    float tempTorsoRot = torso.Rotation += 15;

    rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound, upperArmRotationBound);
    torso.Rotation = Mathf.Clamp(tempTorsoRot, lowerRotationBound, upperTorsoRotationBound);
    };
  }

  public void Up() {
    skeletonAnimation.UpdateLocal += delegate(SkeletonRenderer skeletonRenderer) {
    const float lowerRotationBound = 95.0f;
    const float upperArmRotationBound = 200.0f;
    const float upperTorsoRotationBound = 125.0f;

    float tempArmRot = rightUpperArm.Rotation += 3;
    float tempTorsoRot = torso.Rotation += 1;

    rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound, upperArmRotationBound);
    torso.Rotation = Mathf.Clamp(tempTorsoRot, lowerRotationBound, upperTorsoRotationBound);
    };
  }

  public void Down() {
    skeletonAnimation.UpdateLocal += delegate(SkeletonRenderer skeletonRenderer) {
    const float lowerRotationBound = 95.0f;
    const float upperArmRotationBound = 200.0f;
    const float upperTorsoRotationBound = 125.0f;

    float tempArmRot = rightUpperArm.Rotation -= 3;
    float tempTorsoRot = torso.Rotation -= 1;

    rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound, upperArmRotationBound);
    torso.Rotation = Mathf.Clamp(tempTorsoRot, lowerRotationBound, upperTorsoRotationBound);
    };
  }

  void Start () {
    angle = 0.0f;
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
