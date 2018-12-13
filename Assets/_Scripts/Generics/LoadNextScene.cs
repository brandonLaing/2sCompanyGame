using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{

  public string sceneToLoad;
  public float waitTime;
  public bool loadOnStart;

  private void Start()
  {
    if (!loadOnStart)
      return;

    if (waitTime > 0)
      StartCoroutine(WaitForNextSceneCorout());
    else
      LoadScene(sceneToLoad);
  }

  private IEnumerator WaitForNextSceneCorout()
  {
    yield return new WaitForSeconds(waitTime);
    LoadScene(sceneToLoad);
  }

  public void LoadScene(string sceneToLoad)
  {
    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene(sceneToLoad);
  }
}
