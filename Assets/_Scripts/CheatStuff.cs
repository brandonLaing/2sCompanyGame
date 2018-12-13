using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatStuff : MonoBehaviour
{
  public GameObject playerPrefab;

  private void Start()
  {
    var sb = new StringBuilder();
    sb.AppendLine("Cheet Codes");
    sb.AppendLine("0: toMenu");
    sb.AppendLine("1: toOverworld");
    sb.AppendLine("2: toSlime");
    sb.AppendLine("3: toMole");
    sb.AppendLine("4: Deal 10 damage to boss");
    sb.AppendLine("5: god mode on");
    sb.AppendLine("6: god mode off");


    Debug.Log(sb);

  }
  void Update ()
  {
		if (Input.GetKeyDown(KeyCode.Keypad0))
    {
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("01 Main Menu");
    }
    if (Input.GetKeyDown(KeyCode.Keypad1))
    {
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("Overworld");
    }
    if (Input.GetKeyDown(KeyCode.Keypad2))
    {
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("SlimeArena");
    }
    if (Input.GetKeyDown(KeyCode.Keypad3))
    {
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("MoleArena");
    }
    if (Input.GetKeyDown(KeyCode.Keypad4))
    {
      var bosses = GameObject.FindGameObjectsWithTag("Boss");
      foreach (var boss in bosses)
      {
        if (boss.GetComponent<IDamageable>() != null)
        {
          boss.GetComponent<IDamageable>().Damage(10, "GOD");
        }
      }
    }
    if (Input.GetKeyDown(KeyCode.Keypad5))
    {
      var player = GameObject.FindGameObjectWithTag("Player");
      if (player != null)
      {
        player.GetComponent<HealthSystem>().Invincibility(true);
      }
    }
    if (Input.GetKeyDown(KeyCode.Keypad6))
    {
      var player = GameObject.FindGameObjectWithTag("Player");
      if (player != null)
      {
        player.GetComponent<HealthSystem>().Invincibility(false);
      }
    }
  }
}
