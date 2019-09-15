using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : PhysicsObject {

    public static Controller player;

    public float maxSpeed = 10;
    public float jumpTakeOffSpeed = 10;

    private float curKnockback=0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake () {
        player=this;
        spriteRenderer = GetComponent<SpriteRenderer> ();    
        animator = GetComponent<Animator> ();
    }

    public void ApplyForce(float direction, float mag) {
        curKnockback=direction*mag;
    }

    protected override void ComputeVelocity() {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis ("Horizontal");//horizantal movement
        if(move.x >0f) transform.localScale = new Vector3(3f,3f,1f);
        else if(move.x <0f) transform.localScale = new Vector3(-3f,3f,1f);
        if(Mathf.Abs(curKnockback)>0f){
            move.x += curKnockback;
            curKnockback=0f;
        }   
        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } 
        else if (Input.GetButtonUp ("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        // bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        // if (flipSprite) {
        //     spriteRenderer.flipX = !spriteRenderer.flipX;
        // }

        animator.SetBool ("Grounded", grounded);
        animator.SetFloat ("Horizontal", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}