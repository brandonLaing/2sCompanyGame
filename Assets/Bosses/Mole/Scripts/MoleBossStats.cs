using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleBossState
{
  attacking,
  digging,
  waiting,
  moving,
  rising
}

public class MoleBossStats : MonoBehaviour
{
  public float moveSpeed;
  public MoleBossState currentState = MoleBossState.waiting;
  public GameObject targetingPlayer = null;
  public float underGroundWaitTime;
  public float stopMovingRange;
  public float groundY;
  public float riseSpeed;
  public float undergroundY;
  public float fromUndergroundForce;
  public float fromUndergroundRadius;

}
