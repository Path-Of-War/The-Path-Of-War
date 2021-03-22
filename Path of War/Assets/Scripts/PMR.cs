using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMR : MonoBehaviour
{
    private float timer;

    void Update()
    {
        /*timer -= 1 * Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);*/
    }

    public void DestroyPMR()
    {
        Destroy(gameObject);
    }
}
