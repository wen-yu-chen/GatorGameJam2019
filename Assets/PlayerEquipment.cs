using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEquipment : ScriptableObject{
    public Transform playerPosition;
    public AudioClip soundEffect;

    [SerializeField] string equipName,equipDesc;

    [SerializeField] protected float windupTime, actionTime, falloffTime;
    [SerializeField] protected float sweetSpotStart, sweetSpotDur; 
    [SerializeField] protected GameObject spawnedEffect;
    protected EquipmentEffect effect;

    public delegate void PlayerEquipmentCallback(IEnumerator c);
    public PlayerEquipmentCallback onPhaseFinish;

    public delegate void ActionFinishCallback();
    public ActionFinishCallback onActionFinish;

    public abstract IEnumerator EquipmentWindup();
    public abstract IEnumerator EquipmentAction();
    public abstract IEnumerator EquipmentFinish();
}
