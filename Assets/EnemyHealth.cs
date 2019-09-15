using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] bool boss;
    void OnEnable() {
        currentHP=maxHP;
    }

    public void TakeDamage(int f){
        currentHP-=f;
        if(currentHP<=0) {
            if(boss) ScreenFader.instance.StartFade();
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("ouch");
        EquipmentEffect e = other.GetComponent<EquipmentEffect>();
        if(e!=null){
            switch(e.State){
                case(EffectState.STAB):
                case(EffectState.BASH): 
                case(EffectState.SLASH): 
                case(EffectState.ESLASH): TakeDamage(e.Damage); return;
                case(EffectState.BLOCK): 
                default: return;
            }
        }
    }
}
