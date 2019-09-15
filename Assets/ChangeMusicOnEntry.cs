using UnityEngine;

public class ChangeMusicOnEntry : MonoBehaviour {
    [SerializeField] bool boss;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==11) {
            if(boss) MusicPlayer.instance.StartBoss();
            else MusicPlayer.instance.NextClip();        
        }
        gameObject.SetActive(false);
    }
}