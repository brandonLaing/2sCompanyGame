using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempAttack : MonoBehaviour {

  private PlayerStats ps;
  public Transform playerBody;
  public Transform representation;

  private void Start()
  {
    ps = GetComponent<PlayerStats>();
  }

  void Update () {
    if (Input.GetMouseButtonDown(0))
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
