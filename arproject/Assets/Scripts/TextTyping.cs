using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextTyping : MonoBehaviour
{
    public Text GameOvetText;
    public float typingSpeed;// 타이핑 속도 (초)
    string fullText;

    private void Start()
    {
        fullText = GameOvetText.text;
        GameOvetText.text = ""; // 초기 텍스트를 빈 문자열로 설정

        DisplayText();
    }

    public void DisplayText()
    {
        GameOvetText.DOText(fullText, typingSpeed);
    }
}

