using UnityEngine;
using System.Collections;
using Spine;
using System;
public class SpritePlayer : MonoBehaviour {
  SkeletonAnimation skeletonAnimation;
  public void Start () {
    // Get the SkeletonAnimation component for the GameObject this script is attached to.
    skeletonAnimation = GetComponent<SkeletonAnimation>();
    // Call our method any time an animation fires an event.
    skeletonAnimation.state.Event += Event;
    // A lambda can be used for the callback instead of a method.
    skeletonAnimation.state.End += (state, trackIndex) => {
      Debug.Log("start: " + state.GetCurrent(trackIndex));
    };
    // Queue jump to be played on track 0 two seconds after the starting animation.
    skeletonAnimation.state.AddAnimation(0, "jump", false, 2);
    // Queue walk to be looped on track 0 after the jump animation.
    skeletonAnimation.state.AddAnimation(0, "run", true, 0);
  }
  public void Event (Spine.AnimationState state, int trackIndex, Spine.Event e) {
    Debug.Log(trackIndex + " " + state.GetCurrent(trackIndex) + ": event " + e + ", " + e.Int);
  }
  public void OnMouseDown () {
    // Set jump to be played on track 0 immediately.
    skeletonAnimation.state.SetAnimation(0, "jump", false);
    // Queue walk to be looped on track 0 after the jump animation.
    skeletonAnimation.state.AddAnimation(0, "run", true, 0);
  }
}