using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Shield", menuName = "Equipment/Shield")]
public class ShieldEquip : PlayerEquipment {
    float phaseEnd;
    [SerializeField] int hitsTilFailure;
    [SerializeField] AudioClip blockSound, failureSound;
    bool shieldFailure=false;

    public override IEnumerator EquipmentWindup() {
        Controller.player.maxSpeed = 0f;
        effect = Instantiate(spawnedEffect,playerPosition).GetComponent<EquipmentEffect>();
        effect.transform.localPosition=Vector3.zero;
        phaseEnd = Time.time + windupTime;
        effect.Damage = 0;
        while(Time.time < phaseEnd){ //if hit during here, double damage? stun? knockback? we'll see
            yield return null;
        }

        onPhaseFinish(EquipmentAction());
    }

    public override IEnumerator EquipmentAction() { //indefinite, wait for button release
        float sss = Time.time + sweetSpotStart;
        float sse = sss + sweetSpotDur;
        while(Input.GetButton("Fire1") || Time.time<sse){ //AT LEAST do the sweet spot
            Controller.player.maxSpeed = 1f;
            if(effect.Interactions > hitsTilFailure) {
                Debug.Log("oh shit");
                shieldFailure=true;
                Controller.player.transform.GetComponent<Animator>().SetBool("Recovery",true);
                break;
            }
            if(Time.time > sss && Time.time < sse) effect.State = EffectState.DEFLECT;
            else effect.State = EffectState.BLOCK;

            yield return null;
        }
        onPhaseFinish(EquipmentFinish());
    }

    public override IEnumerator EquipmentFinish() {
        Controller.player.maxSpeed = 0f;
        effect.State=0;
        phaseEnd = Time.time+(shieldFailure?3f:falloffTime);
        if(shieldFailure) {
            Destroy(effect.gameObject);
            effect=null;            
        }
        while(Time.time<phaseEnd){
            yield return null;
        }
        onActionFinish();
        if(!shieldFailure){
            Destroy(effect.gameObject);
            effect=null;
        }

        if(shieldFailure) Controller.player.transform.GetComponent<Animator>().SetBool("Recovery",false);

        shieldFailure=false;
        Controller.player.maxSpeed = 5f;
    }
}