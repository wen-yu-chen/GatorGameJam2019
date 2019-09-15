using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Sword", menuName = "Equipment/SlashingSword")]
public class SwordEquip : PlayerEquipment {
    float phaseEnd;
    [SerializeField] EffectState sweetSpotBehavior=EffectState.REFLECT;
    [SerializeField] EffectState damageBehavior=EffectState.SLASH;
    [SerializeField] AudioClip slashSound;
    [SerializeField] int damage;

    public override IEnumerator EquipmentWindup(){
        effect = Instantiate(spawnedEffect,playerPosition).GetComponent<EquipmentEffect>();
        effect.transform.localPosition=Vector3.zero;
        effect.Damage = damage; 
        effect.gameObject.SetActive(false);
        
        phaseEnd = Time.time + windupTime;
        while(Time.time < phaseEnd){ //if hit during here, double damage? stun? knockback? we'll see
            yield return null;
        }

        onPhaseFinish(EquipmentAction());
    }

    public override IEnumerator EquipmentAction(){
        effect.gameObject.SetActive(true);
        effect.PlaySound(slashSound);
        phaseEnd = Time.time + actionTime;
        float sweetSpot = Time.time + sweetSpotStart;
        float sweetSpotEnd = sweetSpot + sweetSpotDur;
        while(Time.time < phaseEnd){
            if(Time.time>sweetSpot && Time.time < sweetSpotEnd) {
                effect.State = sweetSpotBehavior;
            }
            else {
                effect.State = damageBehavior;
            }
            yield return null;
        }
        onPhaseFinish(EquipmentFinish());
    }

    public override IEnumerator EquipmentFinish(){
        effect.State = EffectState.INACTIVE;
        phaseEnd = Time.time+falloffTime;
        while(Time.time<phaseEnd){
            yield return null;
        }
        onActionFinish();
        Destroy(effect.gameObject);
        effect=null;
    }
}