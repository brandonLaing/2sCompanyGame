using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
  public AnimationState currentAniState;

}

public enum AnimationState
{
  Idle,

  WalkingForward,
  WalkingBack,
  WalkingRigh,
  WalkingLeft,

  RunningForward,
  RunningForwardRight,
  RunningForwardLeft,

  TurningLeft,
  TurningRight

}
