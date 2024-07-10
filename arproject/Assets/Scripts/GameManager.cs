using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    public GameObject ShootManager;
    public GameObject Spawn;
    public GameObject pauseCanvas;

    public AudioClip explo;
    public AudioSource audioSource;

    private bool _WaitForExit = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        GameReset();
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

    public void Explosion()
    {
        audioSource.PlayOneShot(explo);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(int CurScore)
    {
        if (CurScore > PlayerPrefs.GetInt("MaxScore")) PlayerPrefs.SetInt("MaxScore", CurScore);
        SceneManager.LoadScene("EndScene");
    }
    public void GameReset()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        ShootManager.GetComponent<Shoot>().Reset();
        Spawn.GetComponent<Spawn>().Reset();
        
    }
}
