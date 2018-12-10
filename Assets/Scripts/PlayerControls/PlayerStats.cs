/** By: Brandon Laing
 * Created: 10/10/18
 * Last Edited: Bradon Laing 10/10
 *   Created
 *  
 * Description: This will hold all important variables that will need to be referenced by the player
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraControls))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCameraFollowScript))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerStats : MonoBehaviour, IKillable
{
  [Header("Movement")]
  public float playerSpeedWalking = 5F;
  public float playerSpeedRunning = 10F;
  public float playerSpeedStunned = 0F;
  public float playerRotationSpeed;

  [Header("Camera")]
  [Range(1F, 150F)]
  public float cameraVerticleRotationSpeedMulitplier = 100F;
  [Range(1F, 150F)]
  public float cameraHorizontalRotationSpeedMultiplier = 100F;
  [Range(-90F, 0F)]
  public float cameraVerticalMinView = -20F;
  [Range(0F, 90F)]
  public float cameraVerticalMaxView = 50F;

  public bool cameraInvertY = true;
  public bool cameraInvertX = false;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftShift))
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }

  #region Life Function
  public void Kill()
  {
        throw new NotImplementedException("Implement Player Death");
    }
  #endregion

}
