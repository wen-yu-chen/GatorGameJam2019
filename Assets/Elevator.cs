using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour{


    [SerializeField] Transform pointA, pointB;
    [SerializeField] float speed;
    [SerializeField] GameObject door;
    Controller c;
    bool active,finished;

    void OnTriggerStay2D(Collider2D other){
        if(finished) return;
        if(other.gameObject.layer==11) {
            if(Input.GetAxisRaw("Vertical") > 0f){ //ACTIVATE
                c = other.GetComponent<Controller>();
                c.enabled=false;
                c.transform.parent = transform;
                door.SetActive(true);
                active= true;
            }
        }
    }

    void Update(){
        if(finished) return;
        if(active){
            transform.position = Vector2.MoveTowards(transform.position,pointB.position,speed*Time.deltaTime);
            if(Mathf.Approximately(Vector2.Distance(transform.position,pointB.position),0f)){
                c.transform.parent = null;
                c.enabled=true;
                active=false;
                finished=true;
                door.SetActive(false);
            }
        }
    }
}