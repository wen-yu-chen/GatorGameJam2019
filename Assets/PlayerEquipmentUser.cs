using UnityEngine;
using System.Collections;

public class PlayerEquipmentUser : MonoBehaviour {
    [SerializeField] PlayerEquipment[] equipment;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] Animator playerAnim, swordAnim;

    [SerializeField] GameObject[] equips;
    int eind=0;

    bool attacking=false;

    void Update(){
        if(Input.GetButtonDown("Fire1") && !attacking){
            attacking=true;
            equipment[eind].playerPosition = transform;
            equipment[eind].onPhaseFinish = CoroutineStarter;
            equipment[eind].onActionFinish = ResetAction;
            audioSrc.PlayOneShot(equipment[eind].soundEffect);

            playerAnim.SetTrigger("Attack");
            if(eind == 1)swordAnim.SetTrigger("Attack");
            else equips[eind].SetActive(false);
            StartCoroutine(equipment[eind].EquipmentWindup());
        }
        if(Input.GetButtonDown("Fire2") && !attacking){
            equips[eind].SetActive(false);
            eind = eind==0?1:0;
            equips[eind].SetActive(true);
        }
    }

    public void CoroutineStarter(IEnumerator c){
        StartCoroutine(c);
    }

    private void ResetAction(){
        if(eind==0) equips[eind].SetActive(true);
        attacking=false;
    }
}