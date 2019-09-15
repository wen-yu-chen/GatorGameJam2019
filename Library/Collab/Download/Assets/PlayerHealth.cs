using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable {
    [SerializeField] PlayerHealthUI healthBar;
    [SerializeField] int maxHP;
    int currentHP;

    void OnEnable() {
        currentHP = maxHP;
        healthBar.UpdateHealth((float)currentHP/maxHP);
    }

    public void TakeDamage(int d) {
        currentHP -= d;
        healthBar.UpdateHealth((float)currentHP/maxHP);
    }
}
