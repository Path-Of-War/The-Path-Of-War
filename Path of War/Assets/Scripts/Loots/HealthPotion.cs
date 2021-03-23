using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : ALoot
{
    public int healAmount;
    public override void Effect()
    {
        Player.instance.pEffect.Heal(healAmount);
    }
}
