using UnityEngine;
using System.Collections;
using Spine;

public class AnimPlayer : MonoBehaviour {

  [SpineAnimation("idle")]
  public string idleAnimation;

  [SpineAnimation("run")]
  public string moveAnimation;

  [SpineAnimation("attack")]
  public string attackAnimation;

  [SpineAnimation("jump")]
  public string jumpAnimation;

  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
  }

  public void Move() {
    this.skeletonAnimation.AnimationName = moveAnimation;
  }

  public void Idle() {
    this.skeletonAnimation.AnimationName = idleAnimation;
  }

  public void Jump() {
    this.skeletonAnimation.AnimationName = jumpAnimation;
  }

  public void Shoot() {
    this.skeletonAnimation.AnimationName = attackAnimation;
  }


}
