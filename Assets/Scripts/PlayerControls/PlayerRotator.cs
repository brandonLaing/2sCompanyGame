using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
  public Transform playerCamera;
  public Transform playerBody;

  private PlayerStats myStats;

  private float rotationDeadZone = .5F;
  private float rotationSpeed = 100F;

  public float bodyYPlus;
  public float bodyYNegitive;

  public float cameraY;

  private void Start()
  {
    myStats = GetComponent<PlayerStats>();
  }


  private void Update()
  {
    UpdatePlayerRotation();
  }

  private void UpdatePlayerRotation()
  {
    #region Fix cameraEuler
    float angleEulerLimitBodyY = playerBody.transform.eulerAngles.y;

    if (angleEulerLimitBodyY > 180)
    {
      angleEulerLimitBodyY -= 360;
    }

    if (angleEulerLimitBodyY < -180)
    {
      angleEulerLimitBodyY += 360;
    }

    float angleEulerLimitCameraY = playerCamera.transform.eulerAngles.y;

    if (angleEulerLimitCameraY > 180)
    {
      angleEulerLimitCameraY -= 360;
    }

    if (angleEulerLimitCameraY < -180)
    {
      angleEulerLimitCameraY += 360;
    }

    bodyYPlus = angleEulerLimitBodyY + rotationDeadZone;
    bodyYNegitive = angleEulerLimitBodyY - rotationDeadZone;
    cameraY = angleEulerLimitCameraY;
    #endregion

    // check if the body needs to be rotated positive or negitive
    if (bodyYPlus < cameraY)
    {
      Debug.Log("Body needs to rotated negitive");
      playerBody.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);

    }
    if (bodyYNegitive > cameraY)
    {
      Debug.Log("Body Needs to be rotated positive");
      playerBody.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);

    }
  }
}
