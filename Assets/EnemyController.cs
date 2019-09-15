using UnityEngine;
using System.Collections;

public enum EnemyState{
    IDLE, ENGAGED, MOVING, FIRING, HIT
}
public class EnemyController : MonoBehaviour {
    [SerializeField] EnemyState state=EnemyState.IDLE;
    Transform playerTransform;
    [SerializeField] float distanceToMaintain;
    [SerializeField] int fireAmount; //amount of shots to fire
    [SerializeField] float fireRate, fireInterval; //how often to fire, interval bt shots
    [SerializeField] float moveSpeed; //how fast to run

    [SerializeField] Transform gunTransform;
    [SerializeField] ProjectileShooter gun;

    public void Engage(Transform p){
        if(state != EnemyState.IDLE) return;
        state=EnemyState.ENGAGED;
        playerTransform=p;
        Debug.Log("eNGAGED" + playerTransform.name);
        StartCoroutine(EngagedState());
    }

    IEnumerator EngagedState(){ //maintain distance from player
        state = EnemyState.ENGAGED;
        float fireTime = Time.time + fireInterval; bool playerTooClose=false;
        float degPerTick = Quaternion.Angle(Quaternion.LookRotation(Vector3.forward,
            playerTransform.position - gunTransform.position),transform.rotation) / fireInterval;
        while(Time.time < fireTime){
            if(Mathf.Abs(playerTransform.position.x - transform.position.x) < distanceToMaintain){
                playerTooClose=true;
                break;
            }
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward,
            playerTransform.position - gunTransform.position);
            //gunTransform.rotation = Quaternion.RotateTowards(gunTransform.rotation,rotation,500f*Time.deltaTime);
            yield return null;
        }
        if(playerTooClose) StartCoroutine(MovingState(transform.position + 
        (playerTransform.position.x > transform.position.x?Vector3.left:Vector3.right) * distanceToMaintain
        ));
        else StartCoroutine(FiringState());
    }

    IEnumerator MovingState(Vector2 newPosition){
        state = EnemyState.MOVING;
        while(!Mathf.Approximately(Vector2.Distance(transform.position,newPosition),0f)){
            transform.position = Vector2.MoveTowards(transform.position,newPosition,moveSpeed*Time.deltaTime);
            yield return null;
        }
        StartCoroutine(EngagedState());
    }

    IEnumerator FiringState(){
        state = EnemyState.FIRING;
        for(int i=0; i<fireAmount; ++i){
            //fire shot
            gun.FireGun();
            float nextFire = Time.time + fireRate;
            while(Time.time<nextFire){
                yield return null;
            }
        }
        StartCoroutine(EngagedState());
    }
}