using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeatherChest : AArmor
{
    public override void Effect()
    {
        Player.instance.pEquipement.EquipArmor(gameObject);
        Player.instance.UseItem(gameObject);
    }
}
