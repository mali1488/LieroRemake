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

  [SpineSlot]
  public string gunSlot;

  [SpineAttachment(currentSkinOnly: true, slotField: "gunSlot")]
  public string carbineAttachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "gunSlot")]
  public string mp40Attachment;

  [SpineAttachment(currentSkinOnly: true, slotField: "gunSlot")]
  public string thompsonAttachment;

  private SkeletonAnimation skeletonAnimation = null;

  public void Start() {
    //skeletonAnimation.skeleton.SetAttachment(gunSlot, thompsonAttachment);
  }

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
}
