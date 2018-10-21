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
public class SlimeMovementScript : MonoBehaviour, IDamageable, IAttacking, IKillable
{
  [Header("Slime State")]
  public SlimeState myState;

  [Header("Health")]
  public float healthMax = 100;
  public float healthMin = 20;
  public float _health;
  public float Health
  {
    get { return _health; }

    set
    {
      Debug.LogWarning("Health Value " + value);
      _health = value;

      if (value > healthMax)
      {
        _health = healthMax;
        return;
      }

      if (value <= healthMin)
      {
        Kill();
        return;
      }
    }
  }
  public float healthSizeScaler = 10;

  [Header("Damage")]
  public float damagePerSecond = 5;
  public float damageOnImpact; // ?

  [Header("Jumping")]
  public float jumpForce = 5;
  public float jumpForceStepPerDamage = 1;
  public float HitGroundWaitTime = 2;
  public float fallMultiplier = 2.5F;
  private bool jumpRequest = false;
  private bool waitingToLook = false;

  [Header("Rotating")]
  public float rotationRate = 90F;
  public float stopLookingAngle = 2;

  [Header("Other Variables")]
  private readonly float density = 1.3F;
  private float volume;

  [Header("Other Objects")]
  public Transform target;
  private Rigidbody rb;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();

    _health = healthMax;
    UpdateObjectScale();
  }

  private void Update()
  {
    switch (myState)
    {
      case SlimeState.Falling:
        // do nothing
        break;

      case SlimeState.Jumping:
        // do nothing its handled in fixed update
        break;

      case SlimeState.LookingTwardsPlayer:
        LookTowardPlayer();
        break;

      case SlimeState.TouchingGround:
        if (!waitingToLook)
        {
          StartCoroutine(WaitToLookAtPlayer());
        }
        break;
    }
  }

  #region Fixed Physics update
  // Fixed update for physics
  private void FixedUpdate()
  {
    CheckToAccelerateFall();
    CheckToDoJump();
  }

  // checks if there is a request to jump and if there is it jumps
  private void CheckToDoJump()
  {
    if (myState == SlimeState.Jumping)
    {
      // sets do jump to off
      jumpRequest = !jumpRequest;

      // adds force forward, up in proportion to your mass
      rb.AddForce((transform.forward + transform.up) * rb.mass * jumpForce, ForceMode.Impulse);
      // sets new state to falling
      myState = SlimeState.Falling;
    }
  }

  // This if the objects if falling down will accelerate its fall in the hope of making it feel less floaty
  private void CheckToAccelerateFall()
  {
    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }
  }
  #endregion

  #region Looking toward player
  private IEnumerator WaitToLookAtPlayer()
  {
    waitingToLook = true;
    yield return new WaitForSeconds(HitGroundWaitTime);
    myState = SlimeState.LookingTwardsPlayer;
    waitingToLook = false;
  }

  // look towards a target
  private void LookTowardPlayer()
  {
    // sets a target direction on its y transform
    Vector3 tempTargetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
    // gets the direction it needs to look at
    Vector3 direction = (tempTargetPosition - transform.position).normalized;
    // gets the rotation it needs to look towards
    Quaternion lookRotation = Quaternion.LookRotation(direction);

    // rotates towards that location
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationRate);

    // gets the angle between the two objects
    float angleBetween = Vector3.Angle(this.transform.forward, transform.position - tempTargetPosition);

    // checks if its within jump range
    if (angleBetween > 180 - stopLookingAngle && angleBetween < 180 + stopLookingAngle)
    {
      // sets state to jump
      myState = SlimeState.Jumping;
    }
  }
  #endregion

  #region Scale and mass
  // updates the scale of the object based on health
  private void UpdateObjectScale()
  {
    float scaleHealth = (Health / healthMax) * healthSizeScaler;
    transform.localScale = new Vector3(scaleHealth, scaleHealth, scaleHealth);

    SetMass();
  }

  // sets the mass based on the scale
  private void SetMass()
  {
    volume = transform.localScale.x * transform.localScale.x * transform.localScale.x;
    rb.mass = volume * density;
  }
  #endregion

  #region Combat 
  public void Damage(float damage, GameObject obj)
  {
    Debug.Log(gameObject.name + " was damaged for " + damage + " by " + obj.name);

    Health -= damage;

    jumpForce += (damage * jumpForceStepPerDamage);

    UpdateObjectScale();
  }

  // does attack on another IDamageable
  public void Attack(IDamageable other, GameObject otherObj)
  {
    other.Damage(damagePerSecond * Time.deltaTime, gameObject);
  }

  public void ImpactAttack(IDamageable other)
  {
    other.Damage(damageOnImpact, gameObject);
  }

  // what is called when IKillable is killed
  public void Kill()
  {

  }

  #endregion

  #region Debug Testing
  [Header("Debugging")]
  public float testDamage;
  public bool doTestDamage;
  private void TestDoDamage()
  {
    Damage(testDamage, gameObject);

  }

  #endregion

}