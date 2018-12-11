using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundChecker : MonoBehaviour
{
  public SlimeMovementScript sm;
  public Transform tf;
  public bool touchingGround;

  //private void Update()
  //{
  //  transform.position = tf.position;
  //  transform.rotation = tf.rotation;
  //  transform.localScale = tf.localScale;
  //}

  private void OnTriggerEnter(Collider other)
  {
    touchingGround = true;
    if (sm.myState == SlimeState.Falling)
    {
      sm.myState = SlimeState.TouchingGround;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    touchingGround = false;
  }
}
