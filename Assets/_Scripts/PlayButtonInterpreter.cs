using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonInterpreter : MonoBehaviour
{
  public GameObject playerPrefab;
  public string sceneName;
  public Vector3 spawnPosition;

  public void StartGame()
  {
    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene(sceneName, playerPrefab, spawnPosition);
  }
}
