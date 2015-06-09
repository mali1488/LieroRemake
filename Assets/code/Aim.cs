using UnityEngine;
using System.Collections;
using Spine;

public class Aim : MonoBehaviour {

  private Bone rightUpperArm;
  private Bone torso;
  private SkeletonAnimation skeletonAnimation = null;
  private float bazookaAngle = 103.73f;

  private float lowerRotationBound = 95.0f;
  private float upperArmRotationBound = 200.0f;
  private float upperTorsoRotationBound = 125.0f;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
    rightUpperArm = this.skeletonAnimation.skeleton.FindBone("rightUpperArm");
    torso = this.skeletonAnimation.skeleton.FindBone("torso");
  }

  public float GetAngle() {
    return rightUpperArm.Rotation;
  }

  public void SetAngle(int adj) {
    float tempArmRot = rightUpperArm.Rotation + adj*bazookaAngle;
    rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound, upperArmRotationBound);
  }

  public void Up(bool isBazooka) {
    skeletonAnimation.UpdateLocal += delegate(SkeletonRenderer skeletonRenderer) {

      float tempArmRot = rightUpperArm.Rotation += 3;
      float tempTorsoRot = torso.Rotation += 1;

      float offset;
      if(isBazooka) {
        offset = bazookaAngle;
      }else{
        offset = 0;
      }

      rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound + offset, upperArmRotationBound + offset);
      //torso.Rotation = Mathf.Clamp(tempTorsoRot, lowerRotationBound, upperTorsoRotationBound);
    };
  }

  public void Down(bool isBazooka) {
    skeletonAnimation.UpdateLocal += delegate(SkeletonRenderer skeletonRenderer) {

      float tempArmRot = rightUpperArm.Rotation -= 3;
      float tempTorsoRot = torso.Rotation -= 1;

      float offset;
      if(isBazooka) {
        offset = bazookaAngle;
      }else{
        offset = 0;
      }
      rightUpperArm.Rotation = Mathf.Clamp(tempArmRot, lowerRotationBound + offset, upperArmRotationBound + offset);
      torso.Rotation = Mathf.Clamp(tempTorsoRot, lowerRotationBound, upperTorsoRotationBound);
    };
  }
}
