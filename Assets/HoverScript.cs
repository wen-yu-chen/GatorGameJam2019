using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [SerializeField] float freq, amp;
    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        position=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position + Vector2.up * Mathf.Sin(Time.time * freq) * amp;
    }
}
