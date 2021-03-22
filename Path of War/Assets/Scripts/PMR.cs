using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMR : MonoBehaviour
{
    private float timer;
    public Player p;

    private void Start()
    {
        p.target = null;
    }

    void Update()
    {
    }

    public void DestroyPMR()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("setting up target");
            p.target = other.gameObject;
        }
    }
}
