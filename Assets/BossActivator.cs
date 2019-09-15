using UnityEngine;

public class BossActivator :MonoBehaviour {

    [SerializeField] GameObject boss;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==11){
            boss.SetActive(true);
            MusicPlayer.instance.StartBoss();
            gameObject.SetActive(false);
        }   
    }
}