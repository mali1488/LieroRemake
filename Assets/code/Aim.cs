using UnityEngine;
using System.Collections;
using Spine;

public class Aim : MonoBehaviour {

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
}
