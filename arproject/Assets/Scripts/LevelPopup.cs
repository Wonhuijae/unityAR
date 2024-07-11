using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelPopup : MonoBehaviour
{ 
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.DOShakeRotation(1);
        Invoke("wait", 1);
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void wait()
    {
        gameObject.SetActive(false);
    }
}
