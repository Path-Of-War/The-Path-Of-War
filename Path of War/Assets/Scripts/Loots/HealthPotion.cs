using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : ALoot
{
    public override void Effect()
    {
        Debug.Log("hey the function is overrided");
    }
}
