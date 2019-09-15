using UnityEngine;
using System.Collections;

public enum EffectState{ 
    INACTIVE=0,BLOCK,REFLECT,DEFLECT,LAUNCH,SLASH,BASH,ESLASH,STAB
}
public class EquipmentEffect :MonoBehaviour { 
    private AudioSource audsrc;
    private Collider2D col;

    [SerializeField] AudioClip blockSound;

    private EffectState state;
    public EffectState State{
        get{return state;}
        set{
            if(value==0) col.enabled=false;
            else col.enabled=true;
            state=(EffectState)value;}
    }
    private int interactions=0;
    public int Interactions{get{return interactions;}}

    private int damage=2;
    public int Damage{get{return damage;}
    set{damage=value;}}

    void Awake(){
        col=GetComponent<Collider2D>();
        audsrc = GetComponent<AudioSource>();
    }

    void OnEnable(){
        interactions=0;
        state=EffectState.INACTIVE;
    }

    public void PlaySound(AudioClip a){
        audsrc.PlayOneShot(a);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==10){
            ++interactions;
            if(state==EffectState.BLOCK) {
                PlaySound(blockSound);
                Controller.player.ApplyForce(
                    other.GetComponent<BasicProjectile>().Direction
                    ,other.GetComponent<BasicProjectile>().Speed*0.5f);
            }
        } 
    }
}