using UnityEngine;

public class EnemyActivationRange : MonoBehaviour {
    [SerializeField] EnemyController enemy;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==11) {
            Debug.Log("AHHH");
            enemy.Engage(other.transform);
        }
    }
}