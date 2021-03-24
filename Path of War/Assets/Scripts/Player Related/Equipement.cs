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

    GameObject chest = null;
    AArmor achest = null;

    GameObject boots = null;
    AArmor aboots = null;

    GameObject helmet = null;
    AArmor ahelmet = null;

    GameObject gloves = null;
    AArmor agloves = null;

    GameObject shoulder = null;
    AArmor ashoulder = null;

    #region weapon
    public void EquipWeapon(GameObject w)
    {
        if(w.GetComponent<AWeapon>().level <= p.currentLevel) { 
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
    }

    void UnequipWeapon()
    {
        aweapon.gameObject.SetActive(true);
        p.attack -= aweapon.attack;
        p.range -= aweapon.bonusRange;
        p.attackSpeed = aweapon.attackSpeed;
    }
    #endregion

    #region armor

    public void EquipArmor(GameObject w)
    {
        AArmor aw = w.GetComponent<AArmor>();
        if (w.GetComponent<AArmor>().level <= p.currentLevel)
        {
            switch (aw.armorType)
            {
                case AArmor.ArmorType.boots:
                    EquipBoots(w);
                    break;
                case AArmor.ArmorType.chest:
                    EquipChest(w);
                    break;
                case AArmor.ArmorType.gloves:
                    EquipGloves(w);
                    break;
                case AArmor.ArmorType.helmet:
                    EquipHelmet(w);
                    break;
                case AArmor.ArmorType.shoulder:
                    EquipShoulder(w);
                    break;
                default:
                    break;
            }
        }
    }

    #region boots
    void EquipBoots(GameObject a)
    {
        AArmor aa = a.GetComponent<AArmor>();
        if (boots)
        {
            UnequipBoots();
        }
        boots = a;
        aboots =aa;
        a.SetActive(false);
        p.maxHealth += aa.bonusHealth;
        p.armor += aa.bonusArmor;
    }

    void UnequipBoots()
    {
        aboots.gameObject.SetActive(true);
        p.maxHealth -= aboots.bonusHealth;
        p.armor -= aboots.bonusArmor;
    }
    #endregion

    #region helmet
    void EquipHelmet(GameObject a)
    {
        AArmor aa = a.GetComponent<AArmor>();
        if (helmet)
        {
            UnequipHelmet();
        }
        helmet = a;
        ahelmet = aa;
        a.SetActive(false);
        p.maxHealth += aa.bonusHealth;
        p.armor += aa.bonusArmor;
    }

    void UnequipHelmet()
    {
        ahelmet.gameObject.SetActive(true);
        p.maxHealth -= ahelmet.bonusHealth;
        p.armor -= ahelmet.bonusArmor;
    }
    #endregion

    #region chest
    void EquipChest(GameObject a)
    {
        AArmor aa = a.GetComponent<AArmor>();
        if (chest)
        {
            UnequipChest();
        }
        chest = a;
        achest = aa;
        a.SetActive(false);
        p.maxHealth += aa.bonusHealth;
        p.armor += aa.bonusArmor;
    }

    void UnequipChest()
    {
        achest.gameObject.SetActive(true);
        p.maxHealth -= achest.bonusHealth;
        p.armor -= achest.bonusArmor;
    }
    #endregion

    #region gloves
    void EquipGloves(GameObject a)
    {
        AArmor aa = a.GetComponent<AArmor>();
        if (gloves)
        {
            UnequipGloves();
        }
        gloves = a;
        agloves = aa;
        a.SetActive(false);
        p.maxHealth += aa.bonusHealth;
        p.armor += aa.bonusArmor;
    }

    void UnequipGloves()
    {
        agloves.gameObject.SetActive(true);
        p.maxHealth -= agloves.bonusHealth;
        p.armor -= agloves.bonusArmor;
    }
    #endregion

    #region shoulder
    void EquipShoulder(GameObject a)
    {
        AArmor aa = a.GetComponent<AArmor>();
        if (shoulder)
        {
            UnequipShoulder();
        }
        shoulder = a;
        ashoulder = aa;
        a.SetActive(false);
        p.maxHealth += aa.bonusHealth;
        p.armor += aa.bonusArmor;
    }

    void UnequipShoulder()
    {
        ashoulder.gameObject.SetActive(true);
        p.maxHealth -= ashoulder.bonusHealth;
        p.armor -= ashoulder.bonusArmor;
    }
    #endregion

    #endregion
}
