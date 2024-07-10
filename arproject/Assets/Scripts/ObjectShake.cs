using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShake : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().enabled = false;
        btn.GetComponent<Button>().enabled = false;
        Invoke("Shake", 3f);
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake()
    {
        transform.DOShakeRotation(3);
        GetComponent<Text>().enabled = true;
        btn.GetComponent<Button>().enabled = true;
    }
}
