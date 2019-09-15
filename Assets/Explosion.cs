using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public void Deactivate(){
        transform.parent.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        other.GetComponent<IDamageable>().TakeDamage(10);
    }
}
