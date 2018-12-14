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
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Keypad0))
    {
      var playerParent = GameObject.FindGameObjectWithTag("Player");
      if (playerParent != null)
        Destroy(playerParent.GetComponentInParent<HealthSystem>().gameObject);
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("01 Main Menu");

    }
    if (Input.GetKeyDown(KeyCode.Keypad1))
    {
      var playerParent = GameObject.FindGameObjectWithTag("Player");
      if (playerParent != null)
        Destroy(playerParent.GetComponentInParent<HealthSystem>().gameObject);
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("Overworld", playerPrefab, new Vector3(280, 160, 200));
    }
    if (Input.GetKeyDown(KeyCode.Keypad2))
    {
      var playerParent = GameObject.FindGameObjectWithTag("Player");
      if (playerParent != null)
        Destroy(playerParent.GetComponentInParent<HealthSystem>().gameObject);
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("SlimeArena", playerPrefab, new Vector3(210, 105, 340));

    }
    if (Input.GetKeyDown(KeyCode.Keypad3))
    {
      var playerParent = GameObject.FindGameObjectWithTag("Player");
      if (playerParent != null)
        Destroy(playerParent.GetComponentInParent<HealthSystem>().gameObject);
      GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadNewScene("MoleArena", playerPrefab, new Vector3(253, 10, 271));

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
