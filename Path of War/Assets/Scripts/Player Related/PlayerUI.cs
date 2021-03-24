using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider playerHealthBar;

    private void Update()
    {
        playerHealthBar.maxValue = Player.instance.maxHealth;
        playerHealthBar.value = Player.instance.currentHealth;
    }
}
