using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public AudioSource audioSource;
    bool _WaitForExit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_WaitForExit == false)
            {
                showAndroidToastMessage("종료하시려면 한 번 더 누르세요");
                StartCoroutine(WaitInput());
            }
            else
            {
                Application.Quit();
            }
        }
    }

    private void showAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    private IEnumerator WaitInput()
    {
        _WaitForExit = true;
        yield return new WaitForSecondsRealtime(2.5f);
        _WaitForExit = false;
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
