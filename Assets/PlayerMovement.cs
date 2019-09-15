using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour { 
    [SerializeField] private float speed;
    SpriteRenderer spr;

    void Start() {
        spr = GetComponent<SpriteRenderer>();
        spr.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed*Time.deltaTime,
        Input.GetAxisRaw("Vertical") * speed*Time.deltaTime,0f);
    }
}
