using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthSystem))]
public class HealthSystemEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    HealthSystem myHealthSyst = (HealthSystem)target;
    if (GUILayout.Button("Damage"))
    {
      myHealthSyst.Damage(1);
    }

    if (GUILayout.Button("Heal"))
    {
      myHealthSyst.Heal(1);
    }

    if (GUILayout.Button("Heal To Max"))
    {
      myHealthSyst.HealToMax();
    }

    if (GUILayout.Button("God Mode"))
    {
      myHealthSyst.Invincibility(true);
    }

    if (GUILayout.Button("Exit God Mode"))
    {
      myHealthSyst.Invincibility(false);
    }

    if (GUILayout.Button("Kill"))
    {
      myHealthSyst.Kill();
    }
  }
}
