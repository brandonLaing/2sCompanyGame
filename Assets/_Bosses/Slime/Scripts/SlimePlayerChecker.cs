using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlayerChecker : MonoBehaviour
{
  public SlimeMovementScript sm;
  public Transform tf;

	//void Update ()
 // {
 //   transform.position = tf.position;
 //   transform.rotation = tf.rotation;
 //   transform.localScale = tf.localScale;
	//}

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {

      if (other.GetComponent<IDamageable>() != null)
      {
        sm.ImpactAttack(other.GetComponentInParent<IDamageable>());
        return;
      }

      if (other.GetComponentInParent<IDamageable>() != null)
      {

      }
    }

  }

  private void OnTriggerStay(Collider other)
  {
    if (other.tag == "Player")
    {

      IDamageable damageAble = other.GetComponentInParent<IDamageable>();

      if (damageAble != null && other.gameObject != null)
      {
        sm.Attack(other.GetComponentInParent<IDamageable>(), other.gameObject);
      }
      else
      {
        Debug.LogError("hit something in player but it didnt have a damageable");

      }
    }
  }
}
