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
public class SlimeMovementScript : MonoBehaviour, IAttackable
{
  public SlimeState myState;

  public float health;

  private Rigidbody rb;
  public float jumpForce;
  public float jumpForceStep;

  [Header("Don't Change")]
  public float density = 5.0F;
  public float volume;
  public float fallMultiplier = 2.5F;
  private bool jumpRequest;

  public Transform target;

  public float rotationRate = 90F;
  public float stopLookingAngle = 10;

  public float HitGroundWaitTime;
  
  private void Start()
  {
    rb = GetComponent<Rigidbody>();

    // set bas mass mass = volume * density
    // volume = scale^3
    SetMass();
  }

  private void SetMass()
  {
    volume = transform.localScale.x * transform.localScale.x * transform.localScale.x;
    rb.mass = volume * density;
  }

  private void Update()
  {
    if (myState == SlimeState.LookingTwardsPlayer)
    {
      LookTowardPlayer();
    }

    if (myState == SlimeState.Jumping)
    {
      jumpRequest = true;
    }

    if (doTestDamage)
    {
      doTestDamage = !doTestDamage;
      TestDoDamage();
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

  private IEnumerator WaitToLookAtPlayer()
  {
    yield return new WaitForSeconds(HitGroundWaitTime);
    myState = SlimeState.LookingTwardsPlayer;
  }

  private void LookTowardPlayer()
  {
    Debug.Log("Rotating");
    Vector3 tempTargetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
    Vector3 _direction = (tempTargetPosition - transform.position).normalized;
    Quaternion _lookRotation = Quaternion.LookRotation(_direction);

    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationRate);

    Debug.Log(Vector3.Angle(this.transform.forward, transform.position - tempTargetPosition));
    float angleBetween = Vector3.Angle(this.transform.forward, transform.position - tempTargetPosition);

    // 179 > 178 && 179 < 182
    //   
    if (angleBetween > 180 - stopLookingAngle && angleBetween < 180 + stopLookingAngle)
    {
      myState = SlimeState.Jumping;
    }
  }

  private void DoJump()
  {
    rb.AddForce((transform.forward + transform.up) * rb.mass * jumpForce, ForceMode.Impulse);
    myState = SlimeState.Falling;

  }

  public void Damage(float damage)
  {
    health -= damage;

    float scaleHealth = (health / 100) * 10;
    transform.localScale = new Vector3(scaleHealth, scaleHealth, scaleHealth);

  }


  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Ground" && myState == SlimeState.Falling)
    {
      myState = SlimeState.TouchingGround;
      StartCoroutine(WaitToLookAtPlayer());

    }
  }

  #region Debug Testing
  [Header("Debuging")]
  public float testDamage;
  public bool doTestDamage;
  private void TestDoDamage()
  {
    Damage(testDamage);

  }

  #endregion

}
