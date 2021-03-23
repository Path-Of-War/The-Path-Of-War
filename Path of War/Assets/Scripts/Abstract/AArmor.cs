using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AArmor : ALoot
{
    public enum ArmorType
    {
        helmet,
        chest,
        boots,
        shoulder,
        gloves
    }

    public int bonusHealth;
    public int bonusArmor;
    public int level;
    public ArmorType armorType;

    private void Start()
    {
        type = LootType.armor;
    }
}
