using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntrance : MonoBehaviour
{
  public string arenaToLoad;
  public Vector3 position;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.tag == "Player")
    {
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene(arenaToLoad, other.transform, position);
    }
  }
}
