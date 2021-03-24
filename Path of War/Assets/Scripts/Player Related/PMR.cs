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
        if (other.gameObject.tag == "Enemy") { 
            if (other.gameObject.GetComponent<AEnemy>().isDead)
            {
                p.SetTarget(other.gameObject);
                p.agent.stoppingDistance = p.lootRange - 1;
            }
            else
            {
                p.SetTarget(other.gameObject);
                p.agent.stoppingDistance = p.range - 1;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<AEnemy>().isDead)
            {
                p.SetTarget(other.gameObject);
                p.agent.stoppingDistance = p.lootRange - 1;
            }
            else
            {
                p.SetTarget(other.gameObject);
                p.agent.stoppingDistance = p.range-1;
            }
        }
    }
}
