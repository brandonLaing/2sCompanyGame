using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntrance : MonoBehaviour
{
  public string arenaToLoad;
  public Vector3 position;
  public bool loadingNextScene;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.tag == "Player" && !loadingNextScene)
    {
      loadingNextScene = true;
      Debug.Log("player entered " + transform.name);

      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene(arenaToLoad, other.transform.GetComponentInParent<HealthSystem>().transform, position);
    }
  }
}
