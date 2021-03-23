using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public Player p;
    public void Heal(int amount)
    {
        p.currentHealth += amount;
        if(p.currentHealth > p.maxHealth)
            p.currentHealth = p.maxHealth;
        Debug.Log("Player healed for " + amount + " health");
    }

    public void TakeDamage(int amount)
    {
        int dmgAmount = amount - p.armor;
        if(dmgAmount > 0)
            p.currentHealth -= dmgAmount;
        if (p.currentHealth <= 0)
            Die();
    }

    void Die()
    {
        //TODO Dying animation/particule
        Debug.Log("Player Died");
    }
}