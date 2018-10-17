using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
  TouchingGround,
  Jumping,
  Falling,
  LookingTwardsPlayer
}

[RequireComponent(typeof(Rigidbody))]
public class SlimeMovementScript : MonoBehaviour
{
  public SlimeState myState;

  private Rigidbody rb;
  public float jumpForce;

  [Header("Don't Change")]
  public float density = 5.0F;
  public float volume;
  public float fallMultiplier = 2.5F;
  public bool jumpRequest;

  public Transform target;

  public float rotationRate = 90F;

  public float HitGroundWaitTime;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();

    // set bas mass mass = volume * density
    // volume = scale^3
    volume = transform.localScale.x * transform.localScale.x * transform.localScale.x;
    rb.mass = volume * density;
  }

  private void Update()
  {
    if (myState == SlimeState.LookingTwardsPlayer)
    {
      Debug.Log("Rotating");
      Vector3 tempTargetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);

      Vector3 directin = tempTargetPosition - transform.position;
      Quaternion rotation = Quaternion.LookRotation(directin);
      transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationRate * Time.deltaTime);

      transform.LookAt(tempTargetPosition);

      if (Vector3.Angle(this.transform.forward, target.position - transform.position) < 10)
      {
        myState = SlimeState.Jumping;
      }
    } 

    if (myState == SlimeState.Jumping)
    {
      jumpRequest = true;
    }
  }

  private void FixedUpdate()
  {
    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }

    if (jumpRequest)
    {
      jumpRequest = !jumpRequest;
      DoJump();
    }
  }

  private void DoJump()
  {
    rb.AddForce((transform.forward + transform.up) * jumpForce, ForceMode.Impulse);
    myState = SlimeState.Falling;

  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Ground" && myState == SlimeState.Falling)
    {
      myState = SlimeState.TouchingGround;
      StartCoroutine(WaitToLookAtPlayer());

    }
  }

  private IEnumerator WaitToLookAtPlayer()
  {
    yield return new WaitForSeconds(HitGroundWaitTime);
    myState = SlimeState.LookingTwardsPlayer;
  }
}
