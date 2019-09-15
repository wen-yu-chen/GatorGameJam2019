using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable {
    [SerializeField] PlayerHealthUI healthBar;
    [SerializeField] int maxHP;
    [SerializeField] AudioClip onHit,death;

    public Transform lastCheckpoint;

    Animator animator;
    AudioSource audioSrc;
    int currentHP;

    void OnEnable() {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        audioSrc = GetComponent<AudioSource>();
        healthBar.UpdateHealth((float)currentHP/maxHP);
    }

    public void TakeDamage(int d) {

        audioSrc.PlayOneShot(onHit);
        animator.SetTrigger("Hurt");
        currentHP -= d;
        healthBar.UpdateHealth((float)currentHP/maxHP);

        if(currentHP<=0){
            currentHP=maxHP;
            audioSrc.PlayOneShot(death);
            transform.position = lastCheckpoint.position;
        }
    }
}
