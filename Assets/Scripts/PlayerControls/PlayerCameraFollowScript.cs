/** By: Brandon Laing
 * Created: 10/10/18
 * Last Edited: Bradon Laing 10/10
 *   Created
 *  
 * Description: This will follow a give game object and do so based on inspector Variables
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollowScript : MonoBehaviour
{
  public Transform followObject;
  public Transform followingObject;

  [Header("Follow Speed")]
  public float slowFollowSpeed = 5.0F;
  public float fastFollowSpeed = 20.0F;
  public float rangeSlow = 1.0F;
  public float rangeFast = 10.0F;
  public float stopFollowingRange = .01F;

  [Header("Snapping")]
  public bool ableToSnap = true;

  
  private void Update()
  {
    CheckHowToMove();
  }

  private void CheckHowToMove()
  {
    // get the distance
    float distance = Vector3.Distance(followingObject.position, followObject.transform.position);

    // check if its below stop follow range
    if (distance < stopFollowingRange) { return; }

    // check if its bellow slow move range and if it is move slowly
    if (distance < rangeSlow)
    {
      followingObject.position = Vector3.MoveTowards(followingObject.position, followObject.position, slowFollowSpeed * Time.deltaTime);
      return;
    }

    // check if its bellow fast range or it cant snap move fast
    if (distance < rangeFast || !ableToSnap)
    {
      followingObject.position = Vector3.MoveTowards(followingObject.position, followObject.position, fastFollowSpeed * Time.deltaTime);
      return;
    }
    // if you arent in range to move fast and you can snap snap to position
    else
    {
      followingObject.position = followObject.position;
      return;

    }
  }
}
