using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControls : MonoBehaviour
{
  private PlayerStats myStats;

  public Transform cameraRootTransform;
  public Transform cameraTransform;

  public float mouseX;
  public float mouseY;

  private float cameraInvertYFloat;
  private float cameraInvertXFloat;

  private void Start()
  {
    myStats = GetComponent<PlayerStats>();
  }

  private void Update()
  {
    CameraXRotation();
    CameraYRotation();
  }

  // controls the X rotation
  private void CameraXRotation()
  {
    if (myStats.cameraInvertX)
    {
      cameraInvertXFloat = -1;
    }
    else
    {
      cameraInvertXFloat = 1;
    }

    mouseX = Input.GetAxis("Mouse X");

    cameraRootTransform.Rotate(Vector3.up, mouseX * cameraInvertXFloat * myStats.cameraHorizontalRotationSpeedMultiplier * Time.deltaTime, Space.World);
  }

  // controls the Y rotation
  private void CameraYRotation()
  {
    mouseY = Input.GetAxis("Mouse Y");

    float angelEulerLimit = cameraTransform.eulerAngles.x;

    if (angelEulerLimit > 180)
    {
      angelEulerLimit -= 360;
    }

    if (angelEulerLimit < -180)
    {
      angelEulerLimit += 360;
    }

    if (myStats.cameraInvertY)
    {
      cameraInvertYFloat = -1;
    }
    else
    {
      cameraInvertYFloat = 1;
    }


    float targetRotation = angelEulerLimit + mouseY * cameraInvertYFloat * myStats.cameraVerticleRotationSpeedMulitplier * Time.deltaTime;

    if (targetRotation < myStats.cameraVerticalMaxView && targetRotation > myStats.cameraVerticalMinView)
    {
      cameraTransform.eulerAngles += new Vector3(mouseY * cameraInvertYFloat * myStats.cameraVerticleRotationSpeedMulitplier * Time.deltaTime, 0, 0);

    }
  }
}
