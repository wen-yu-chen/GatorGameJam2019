using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    [SerializeField] AudioClip[] loopPortions;
    [SerializeField] AudioClip bossLoop;
    bool bossFight;

    float[] loopLengths;
    AudioSource audioSource;
    int clip=0;
    int queuedClip=0;
    // Start is called before the first frame update
    void Awake()
    {
        instance=this;
        loopLengths = new float[loopPortions.Length];
        for(int i=0; i<loopPortions.Length;++i){
            loopLengths[i] = loopPortions[i].length;
        }
        audioSource=GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = loopPortions[clip];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update() {
        if(bossFight) return;
        if(queuedClip!=clip && !(audioSource.time < loopLengths[clip])){
            clip=queuedClip;
            audioSource.clip = loopPortions[clip];
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    public void StartBoss(){
        audioSource.clip = bossLoop;
        audioSource.loop=true;
        audioSource.Play();
        bossFight=true;
    }

    public void NextClip(){
        if(bossFight) return;
        if(queuedClip!=clip) return;
        audioSource.loop = false;
        queuedClip = clip+1 < loopPortions.Length ? clip+1 : 0;
    }

    public void PreviousClip(){
        if(bossFight) return;
        if(queuedClip!=clip) return;
        audioSource.loop = false;
        queuedClip = clip-1 < 0 ? loopPortions.Length-1 : clip-1;
    }
}
