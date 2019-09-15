using UnityEngine;

public class Checkpoint :MonoBehaviour {

    [SerializeField] Transform checkpointTransform;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<PlayerHealth>()!=null){
            collider.GetComponent<PlayerHealth>().lastCheckpoint = checkpointTransform;
        }
    }
}