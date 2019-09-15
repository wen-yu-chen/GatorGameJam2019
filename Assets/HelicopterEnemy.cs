using UnityEngine;
using System.Collections;

public class HelicopterEnemy : MonoBehaviour {
    //FIRE ONE ROCKET, FIRE A FEW LASERS, GATLING GUN
    [SerializeField] float attackInterval; //time between attacks
    [SerializeField] int lasersToFire, bulletsToFire;
    [SerializeField] float laserBarrageDelay, bulletDelay;
    [SerializeField] ProjectileShooter[] guns; //0-bullet, 1-laser, 2-rocket
    [SerializeField] Transform[] gunTransforms;

    void OnEnable(){
        gunTransforms = new Transform[3];
        for(int i=0; i<3; ++i) gunTransforms[i] =guns[i].transform;
        StartCoroutine(DelayTilGatling());
    }

    IEnumerator DelayState(){
        float finishTime = Time.time + attackInterval;
        while(Time.time<finishTime){
            yield return null;
        }
        float select = Random.Range(0f,1f);
        if(select < 0.3f) StartCoroutine(FireRocket());
        else if(select >= 0.3f && select < 0.66f) StartCoroutine(FireGatling());
        else StartCoroutine(FireLasers());
    }

    IEnumerator DelayTilGatling(){
        float finishTime = Time.time + attackInterval*0.5f;
        while(Time.time<finishTime){
            yield return null;
        }
        StartCoroutine(FireGatling());
    }

    IEnumerator FireRocket(){
        guns[2].FireGun();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DelayState());
    }

    IEnumerator FireLasers(){
        for(int i=0; i<lasersToFire; ++i){
            //fire shot
            guns[1].FireGun();
            float nextFire = Time.time + laserBarrageDelay;
            while(Time.time<nextFire){
                yield return null;
            }
        }
        StartCoroutine(DelayState());
    }

    IEnumerator FireGatling(){
        for(int i=0; i<bulletsToFire; ++i){
            //fire shot
            guns[0].FireGun();
            float nextFire = Time.time + bulletDelay;
            while(Time.time<nextFire){
                Quaternion r =Quaternion.LookRotation(Vector3.forward,Controller.player.transform.position - gunTransforms[0].position);
                gunTransforms[0].rotation =Quaternion.RotateTowards(gunTransforms[0].rotation,r,120f*Time.deltaTime);
                yield return null;
            }
        }
        StartCoroutine(DelayState());
    }
}