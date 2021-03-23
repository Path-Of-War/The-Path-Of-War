using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapiere : AWeapon
{
    public override void Effect()
    {
        Player.instance.pEquipement.EquipWeapon(gameObject);
        Player.instance.UseItem(gameObject);
    }
}
