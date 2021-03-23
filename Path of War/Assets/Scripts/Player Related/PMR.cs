using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMR : MonoBehaviour
{
    private float timer;
    public Player p;

    private void Start()
    {
        p = Player.instance;
        p.agent.stoppingDistance = 0;
        p.SetTarget(null);
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
            p.SetTarget(other.gameObject);
            p.agent.stoppingDistance = p.range/2;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            p.SetTarget(other.gameObject);
            p.agent.stoppingDistance = p.range/2;
        }
    }
}
