using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader :MonoBehaviour{
    public static ScreenFader instance;
    [SerializeField] CanvasGroup group;

    public void StartFade(){
        StartCoroutine(fade());
    }
    public IEnumerator fade(){
        while(!Mathf.Approximately(group.alpha,1f)){
            group.alpha = Mathf.MoveTowards(group.alpha,1f,0.1f * Time.deltaTime);
            yield return null;
        }
    }
}