using UnityEngine;
using System.Collections;
using Spine;

public class AnimPlayer : MonoBehaviour {
  string currentAnimation = "";

  private SkeletonAnimation skeletonAnimation = null;

  public void Setup(SkeletonAnimation skeletonAnimation) {
    this.skeletonAnimation = skeletonAnimation;
  }

  public void Move() {
    SetAnimation("run", true);
  }

  public void Idle() {
    SetAnimation("idle", true);
  }

  public void Jump() {
    SetAnimation("jump", true);
  }

  public void Shoot() {
    SetAnimation("attack", true);
  }

  public void FallBack() {
    SetAnimation("fallBackwards", false);
  }

  public void FallForward() {
    SetAnimation("fallForwards", false);
  }

  void SetAnimation(string name, bool loop) {
    if(name == currentAnimation) {
      return;
    }

    skeletonAnimation.state.SetAnimation(0, name, loop);
    currentAnimation = name;
  }
}
