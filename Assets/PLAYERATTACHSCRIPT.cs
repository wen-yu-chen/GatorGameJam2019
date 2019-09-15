using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERATTACHSCRIPT : MonoBehaviour
{
    [SerializeField] Transform player;

    void Awake()
    {
        transform.parent = player;
        transform.localPosition = Vector3.zero + Vector3.forward * -10f;        
    }
}
