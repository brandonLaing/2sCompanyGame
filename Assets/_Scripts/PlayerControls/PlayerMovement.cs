/** By: Brandon Laing
 * Created: 10/10/18
 * Last Edited: Bradon Laing 10/10
 *   Created
 *  
 * Description: This will control the basic WASD movement of the player and their move speed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public MovementState moveState;
  public bool CanChageState = true;

  public float moveSpeed;

  private PlayerStats myStats;
  public Transform playerBody;

  private void Start()
  {
    myStats = GetComponent<PlayerStats>();
  }

  private void Update()
  {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveState = MovementState.running;
        }
        else
        {
            moveState = MovementState.walking;
        }
    CheckMoveState();
    
    //MovePlayer();
  }
    private void FixedUpdate()
    {
    MovePlayer();

    }

    // changes the move speed based on the type of movement you are doing
    private void CheckMoveState()
  {
    if (moveState == MovementState.walking)
    {
      moveSpeed = myStats.playerSpeedWalking;
      return;
    }

    if (moveState == MovementState.running)
    {
      moveSpeed = myStats.playerSpeedRunning;
      return;
    }

    if (moveState == MovementState.stunned)
    {
      moveSpeed = myStats.playerSpeedStunned;
    }
  }

  // moves the player
  private void MovePlayer()
  {
    Vector3 targetDirection = new Vector3();

    
    if (Input.GetKey(KeyCode.W))
    {
      targetDirection += playerBody.forward;
    }
    if (Input.GetKey(KeyCode.S))
    {
      targetDirection += -playerBody.forward;
    }
    if (Input.GetKey(KeyCode.A))
    {
      targetDirection += -playerBody.right;
    }
    if (Input.GetKey(KeyCode.D))
    {
      targetDirection += playerBody.right;
    }
    
    playerBody.position += targetDirection.normalized * moveSpeed * Time.deltaTime;
  }

  // Set a players move state to something and lock it to that for an ammount of time
  public void ForceSetMovementState(MovementState forcedState, float forcedDuration)
  {
    CanChageState = false;
    moveState = forcedState;
    StartCoroutine(ForceStateCoRoutine(forcedDuration));
  }
  private IEnumerator ForceStateCoRoutine(float forcedDuration)
  {
    yield return new WaitForSeconds(forcedDuration);
    CanChageState = true;
  }
}

// movement states for the player
public enum MovementState
{
  walking,
  running,
  stunned
}
