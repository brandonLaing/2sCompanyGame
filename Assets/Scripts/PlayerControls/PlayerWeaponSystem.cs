using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
  public WeaponInfo[] weapons;
  public WeaponInfo currentWeapon;

  public Transform shootPoint;

  private void Start()
  {
    currentWeapon = weapons[0];
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      switch (currentWeapon.weaponType)
      {
        case WeaponType.Melee:
          DoMeleeAttack();
          break;

        case WeaponType.Ranged:
          DoRangedAttack();
          break;
      }
    }
  }

  public void DoMeleeAttack()
  {
    // based on type of attack start the animation then check the area and do the damage
  }

  public void DoRangedAttack()
  {
    // check if the current weapon has ammo
    // raycast from front shoot point
    // damage anything if it hits something that can be damage
  }
}

public enum WeaponType
{
  Melee, Ranged
}
public enum AttackType
{
  Light, Heavy
}

[CreateAssetMenu]
public class WeaponInfo : ScriptableObject
{
  public WeaponType weaponType;
  public AttackType attackType;

  public float damage;
  public float ammoCountCurrent;
  public float ammoCountMax;
  public float reloadTime;
  public GameObject weaponModle;

}