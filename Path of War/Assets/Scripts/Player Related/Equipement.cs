using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    public Player p;
    [SerializeField]
    GameObject weapon = null;
    AWeapon aweapon = null;

    public void EquipWeapon(GameObject w)
    {
        if (weapon)
        {
            UnequipWeapon();
        }
        weapon = w;
        aweapon = w.GetComponent<AWeapon>();
        w.SetActive(false);
        p.attack += aweapon.attack;
        p.range += aweapon.bonusRange;
        p.attackSpeed = aweapon.attackSpeed;
    }

    void UnequipWeapon()
    {
        aweapon.gameObject.SetActive(true);
        p.attack -= aweapon.attack;
        p.range -= aweapon.bonusRange;
        p.attackSpeed = aweapon.attackSpeed;
    }
}
