using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : ATempObstacle
{
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(gameObject);
        }
    }

}
