using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBossAttack : MonoBehaviour
{
  public float damage;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.GetComponentInParent<IDamageable>() != null)
    {
      other.GetComponentInParent<IDamageable>().Damage(damage, transform.name);
    }
  }
}
