using UnityEngine;

public class EnergyProjectile : BasicProjectile{

    //BALLISTICS - can be SLASHED REFLECTED DEFLECTED and BLOCKED
    //ENERGY - can be ESLASHED EREFLECTED and BLOCKED
    //ROCKETS - can be BASHED DEFLECTED and BLOCKED, will detonate if SLASHED
    void OnTriggerEnter2D(Collider2D other){
        if(!playerAligned && other.gameObject.layer ==12) return;
        if(playerAligned && other.gameObject.layer==11) return;
        if(other.gameObject.layer==2) return;
        if(other.gameObject.layer==10) return;
        EquipmentEffect e = other.GetComponent<EquipmentEffect>();
        if(e!=null){
            switch(e.State){
                case(EffectState.ESLASH): e.PlaySound(a); FlipAlignment(4f); return;
                case(EffectState.BLOCK): gameObject.SetActive(false); return; 
                default: return;
            }
        }
        else{
            if(other.GetComponent<IDamageable>()!=null) 
                other.GetComponent<IDamageable>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}