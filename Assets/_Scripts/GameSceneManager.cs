using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
  public string startScene;
  public string currentScene = string.Empty;

  private Scene scene;

  public delegate void WaitThenDo();

  private Scene ActiveScene
  {
    get
    {
      return SceneManager.GetSceneByName(currentScene);
    }
  }

  private void Start()
  {
    if (currentScene == string.Empty)
    {
      LoadNewScene(startScene);
    }
  }

  public void LoadNewScene(string newScene)
  {
    if (currentScene != string.Empty)
      SceneManager.UnloadSceneAsync(currentScene);

    SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

    currentScene = newScene;
    Debug.Log(ActiveScene.name);

    WaitThenSetActive(SetSceneActive);

    
  }

  public void SetSceneActive()
  {

  }

  public void LoadNewScene(string newScene, Transform player, Vector3 spawnLocation)
  {
    player.parent = this.transform;

    LoadNewScene(newScene);

    player.position = spawnLocation;
    player.parent = null;
  }

  private IEnumerator WaitThenSetActive(WaitThenDo methodToDo)
  {
    yield return new WaitForSeconds(1);
    methodToDo();
  }
}
