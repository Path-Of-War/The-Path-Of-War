using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider playerHealthBar;

    public Slider playerManaBar;

    private void Update()
    {
        playerHealthBar.maxValue = Player.instance.maxHealth;
        playerHealthBar.value = Player.instance.currentHealth;
        playerManaBar.maxValue = Player.instance.maxMana;
        playerManaBar.value = Player.instance.currentMana;
    }
}
