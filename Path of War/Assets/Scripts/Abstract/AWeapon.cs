using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWeapon : ALoot
{
    public int attack;
    public float bonusRange;
    public float attackSpeed;
    public bool isUsed;
    public int level;
    private void Start()
    {
        type = LootType.weapon;
    }
}
