using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBossMovement : MonoBehaviour
{

  public MoleBossStats stats;
  public CapsuleCollider _bodyCollider;
  public bool waiting = false;

  private MoleBossState CurrentState
  {
    get
    {
      return stats.currentState;
    }
    set
    {
      if (value == MoleBossState.rising)
      {
        Rising(true);
      }

      if (value == MoleBossState.attacking)
      {
        Rising(false);
      }

      if (value == MoleBossState.digging)
      {
        Rising(true);
      }

      if (value == MoleBossState.waiting)
      {
        Rising(false);
      }

      stats.currentState = value;
    }
  }


  public delegate void Del(MoleBossState state);

  private void Start()
  {
    stats = GetComponent<MoleBossStats>();
    _bodyCollider = GetComponentInChildren<CapsuleCollider>();
  }

  private void Update()
  {
    DecisionLogic();
  }

  private void DecisionLogic()
  {
    if (stats.targetingPlayer == null)
    {
      stats.targetingPlayer = GameObject.FindGameObjectWithTag("Player");
      return;
    }
    else if (!waiting)
    {
      if (CurrentState == MoleBossState.waiting)
      {
        StartCoroutine(WaitSwitchState(stats.underGroundWaitTime, SwitchState, MoleBossState.moving));
        return;
      }

      if (CurrentState == MoleBossState.moving)
      {
        MoveTowardsPlayer();
        return;
      }

      if (CurrentState == MoleBossState.rising)
      {
        RaiseMole();
        return;
      }

      if (CurrentState == MoleBossState.attacking)
      {
        Vector3 tempTargetPos = stats.targetingPlayer.transform.position;
        tempTargetPos.y = transform.position.y;
        transform.LookAt(tempTargetPos);
        AttackPlayer();
        return;
      }

      if (CurrentState == MoleBossState.digging)
      {
        SinkMole();
        return;
      }
    }
  }

  private void AttackPlayer()
  {
    StartCoroutine(WaitSwitchState(2, SwitchState, MoleBossState.digging));
  }

  private void RaiseMole()
  {
    Vector3 newPos = new Vector3(transform.position.x, transform.position.y + stats.riseSpeed * Time.deltaTime, transform.position.z);

    if (newPos.y > stats.groundY)
    {
      newPos.y = stats.groundY;
      CurrentState = MoleBossState.attacking;
    }

    transform.position = newPos;
  }

  private void SinkMole()
  {
    Vector3 newPos = new Vector3(transform.position.x, transform.position.y - stats.riseSpeed * Time.deltaTime, transform.position.z);

    if (newPos.y < stats.undergroundY)
    {
      newPos.y = stats.undergroundY;
      CurrentState = MoleBossState.waiting;
    }

    transform.position = newPos;
  }

  private void Rising(bool isRising)
  {
    _bodyCollider.isTrigger = isRising;
  }

  private void MoveTowardsPlayer()
  {
    Vector3 tempPlayerPosition = stats.targetingPlayer.transform.position;
    tempPlayerPosition.y = transform.position.y;
    transform.position = Vector3.MoveTowards(transform.position, tempPlayerPosition, stats.moveSpeed * Time.deltaTime);

    if (Vector3.Distance(transform.position, tempPlayerPosition) < stats.stopMovingRange)
    {
      CurrentState = MoleBossState.rising;
    }
  }

  private void SwitchState(MoleBossState newState)
  {
    CurrentState = newState;
  }

  private IEnumerator WaitSwitchState(float waitTime, Del switchState, MoleBossState newState)
  {
    waiting = true;
    yield return new WaitForSeconds(waitTime);
    waiting = false;
    switchState(newState);
  }

  private void OnTriggerStay(Collider other)
  {

    if (other.tag == "Player")
    {
    Debug.Log("Hit Player");
      other.GetComponent<Rigidbody>().AddForce(Vector3.up * stats.fromUndergroundForce);
    }
  }

}