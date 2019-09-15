using UnityEngine;

public class RocketProjectile : BasicProjectile{



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
                case(EffectState.BASH): direction += Random.Range(-0.5f,0.5f) * Vector2.up;
                FlipAlignment(2f); return;
                case(EffectState.DEFLECT): e.PlaySound(a);FlipAlignment(4f); return;
                case(EffectState.BLOCK): Detonate(false); return; 
                case(EffectState.REFLECT):
                case(EffectState.SLASH): Detonate(true); return; 
                default: return;
            }
        }

        Detonate(true);
    }

    private void Detonate(bool normal){
        speed=0f; spriteRenderer.enabled=false;
        if(normal) transform.GetChild(1).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(true);
    }
}