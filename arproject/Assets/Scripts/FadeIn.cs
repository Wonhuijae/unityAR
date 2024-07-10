using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeIn : MonoBehaviour
{
    float targetFadeValue;
    public GameObject OverText;

    // Start is called before the first frame update
    void Start()
    {
        targetFadeValue = 1f;
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().DOFade(targetFadeValue, 3);
        if(GetComponent<Image>().color.a == targetFadeValue)
        {
            OverText.GetComponent<TextTyping>().DisplayText();
        }
    }
}
