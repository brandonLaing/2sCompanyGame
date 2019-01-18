
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
  public Transform playerCamera;
  public Transform playerBody;

  private PlayerStats myStats;

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
    playerBody.eulerAngles = new Vector3(playerBody.eulerAngles.x, playerCamera.eulerAngles.y, playerCamera.eulerAngles.z);

  }
}