using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALoot : MonoBehaviour
{
    public Sprite spr;
    public string lootName;
    bool isInInventory = false;

    public virtual void Effect()
    {
        Debug.Log(name);
    }

    public virtual void OnClick()
    {
        if (isInInventory)
            Effect();
        else {
            isInInventory = true;
            Player.instance.inventoryData.Add(gameObject);
            Player.instance.Itemlooted(gameObject);
        }
    }
}
