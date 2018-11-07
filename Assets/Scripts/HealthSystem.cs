using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
  [Header("Health Settings")]
  public float healthMin;
  public float healthMax;

  [Space(10)]
  public float healthPerSecond;
  public bool onlyInCombat;
  public bool inCombat;

  [Space(10)]
  private float _health;
  public float Health
  {
    get { return _health; }
    set
    {
      if (!damageImmune)
      {
        if (value < healthMin)
        {
          Kill();
        }
        if (value > healthMax)
        {
          _health = healthMax;
        }

        _health = value;
      }
    }
  }

  [Header("Other")]
  public GameObject deathEffect;

  [Header("Debug Features")]
  private bool damageImmune;

  // heals for that ammount
  public void Heal(float heal)
  {
    Health += heal;
  }

  // heals to max 
  public void HealToMax()
  {
    Health = healthMax;
  }

  // damages for ammount
  public void Damage(float damage, string attacker)
  {
    Health -= damage;
  }

  // kills objects
  public virtual void Kill()
  {
    if (deathEffect != null)
    {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    foreach (Transform child in transform)
    {
      GameObject.Destroy(child.gameObject);
    }

    Destroy(this.gameObject);
  }

  public void Invincibility(bool godMode)
  {
    if (godMode)
    {
      damageImmune = true;
      HealToMax();
    }
    else
    {
      damageImmune = false;
    }
  }
}
