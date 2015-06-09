/// <summary>
/// Sets the animation for the player.
/// </summary>
using UnityEngine;
using System.Collections;
using Spine;

public class AnimPlayer : MonoBehaviour {
  private string currentAnimation = "";
  private bool isBazooka = false;

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
    SetAnimation("fallBackwards", true);
  }

  public void FallForward() {
    SetAnimation("fallForwards", false);
  }

  public void SetBazooka(bool isBazooka) {
    this.isBazooka = isBazooka;
  }
  void SetAnimation(string name, bool loop) {
    if(isBazooka &&  !(name.Equals("fallForwards") || name.Equals("fallBackwards"))) {
      name += "Bazooka";
    }
    if(name == currentAnimation) {
      return;
    }

    skeletonAnimation.state.SetAnimation(0, name, loop);
    currentAnimation = name;
  }
}
