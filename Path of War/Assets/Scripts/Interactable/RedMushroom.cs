using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : AInteractable
{
    public GameObject parent;
    private void Update()
    {
        if(items.Count == 0)
        {
            Destroy(parent);
        }
    }
}
