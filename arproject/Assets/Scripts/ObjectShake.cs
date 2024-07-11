using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShake : MonoBehaviour
{
    public Button btn;
    public float typingSpeed = 3;
    Text text1;
    string fullText;
    // Start is called before the first frame update
    private void Start()
    {
        text1 = gameObject.GetComponent<Text>();
        fullText = text1.text;
        text1.text = "";

        btn.GetComponent<Button>().enabled = false;
        Invoke("ShowBtn", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowBtn()
    {
        btn.GetComponent<Button>().enabled = true;
        text1.enabled = true;
        text1.DOText(fullText, typingSpeed);
    }
}
