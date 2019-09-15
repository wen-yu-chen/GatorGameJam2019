using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    [SerializeField] RectTransform healthBar;
    Image image;

    void Awake() {
        image = healthBar.GetComponent<Image>();
    }

    public void UpdateHealth(float h){
        healthBar.localScale = new Vector3(h,1f,1f);

        if (h <= 0.2) {
            image.color = Color.red;
        }
        //hp less than 50%
        else if (h <= 0.5) {
            image.color = Color.yellow;
        }
    }
}
