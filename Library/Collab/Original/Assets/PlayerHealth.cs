using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //[SerializeField] PlayerHealthUI healthBar;
    [SerializeField] int maxHP;
    int currentHP;

    void OnEnable(){
        currentHP=maxHP;
        PlayerHealthUI.instance.UpdateHealth(1f);
    }

    public void TakeDamage(int d){
        currentHP-=d;
        PlayerHealthUI.instance.UpdateHealth((float)currentHP/maxHP);
    }
}
