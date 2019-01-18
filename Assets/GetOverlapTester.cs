using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOverlapTester : MonoBehaviour
{
  public List<GameObject> hittingObjects;
  public bool sphere;
  public bool box;

  private void Update()
  {
    hittingObjects = new List<GameObject>();

    if (sphere)
    {
      foreach (Collider obj in  Physics.OverlapSphere(transform.position, .5F))
      {
        if (obj.gameObject != this.gameObject)
        {
          hittingObjects.Add(obj.gameObject);
        }
      }
    }

    if (box)
    {
      foreach (Collider obj in Physics.OverlapBox(transform.position + transform.forward,
        transform.localScale / 2,
        transform.rotation))
      {
        if (obj.gameObject != this.gameObject)
        {
          hittingObjects.Add(obj.gameObject);
        }
      }
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere((transform.position + transform.forward), .5F);
  }
}
