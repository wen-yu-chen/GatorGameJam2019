using UnityEngine;

public class ProjectileShooter : MonoBehaviour {
    AudioSource aud;

    [SerializeField] GameObject projPrefab;
    [SerializeField] float speed;
    [SerializeField] Transform initPos, gunEnd;
    [SerializeField] float accuracy;
    [SerializeField] AudioClip gunSound;

    float nextFire;

    void Awake(){
        if(GetComponent<AudioSource>()) aud = GetComponent<AudioSource>();
        else{
            aud = gameObject.AddComponent<AudioSource>();
            aud.loop=false; aud.playOnAwake=false;
            aud.clip = gunSound;
        }
    }

    public void FireGun(){
        BasicProjectile p = Instantiate(projPrefab,gunEnd.position,Quaternion.identity).GetComponent<BasicProjectile>();
        p.InitializeProjectile(speed, gunEnd.position - initPos.position + Vector3.up * Random.Range(-accuracy,accuracy),2);
        aud.PlayOneShot(gunSound);
    }
}