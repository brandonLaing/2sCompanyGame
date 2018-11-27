using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackMode
{
  Melee, Ranged
}

public class PlayerTempAttack : MonoBehaviour {

  private PlayerStats ps;
  public Transform playerBody;
  public Transform representation;

  public Transform gunBarrle;

  public AttackMode currentAttackMode;

  private void Start()
  {
    ps = GetComponent<PlayerStats>();
  }

  void Update () {
    if (Input.GetMouseButtonDown(0))
    {
      if (currentAttackMode == AttackMode.Melee)
      {
        Collider[] hitObjects = Physics.OverlapSphere(playerBody.transform.position + transform.forward, 2);
        foreach (Collider obj in hitObjects)
        {
          IDamageable damageable = obj.GetComponentInParent<IDamageable>();
          if (damageable != null && damageable != ps)
          {
            ps.Attack(damageable, obj.gameObject);
          }
        }
      }
      else if (currentAttackMode == AttackMode.Ranged)
      {
        Ray ray = new Ray(gunBarrle.position, gunBarrle.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10F ))
        {
          if (hit.collider.GetComponentInParent<IDamageable>() != null)
          {
            hit.collider.GetComponentInParent<IDamageable>().Damage(10, transform.name);
          }
        }
      }
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;

    if (Input.GetMouseButton(0))
    {
      Gizmos.DrawWireSphere(playerBody.transform.position + playerBody.transform.forward, 2);

    }
  }
}
