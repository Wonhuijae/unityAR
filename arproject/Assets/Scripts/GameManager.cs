using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    public GameObject ShootManager;
    public GameObject Spawn;
    public GameObject pauseCanvas;
    public GameObject gun;

    public GameObject[] hearts;
    public AudioClip explo;
    public AudioSource audioSource;

    public Text ScoreText;
    public Text MaxText;

    public Text timeTxt;

    private bool _WaitForExit = false;
    private bool _Running = true;

    int score;
    int maxScore;
    int hp;

    int m;
    float s;

    float gameTime;
    float audioLength;

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
        audioLength = explo.length;
        
    }

    private void OnEnable()
    {
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

        if(hp <= 0)
        {
            _Running = false;
            Invoke("GameOver", 2);
        }
        if (!_Running) return;

        gameTime += Time.deltaTime;
        m = (int)gameTime / 60;
        s = gameTime % 60;
        timeTxt.text = $"{m} : {s.ToString("0")}";


        Spawn.GetComponent<Spawn>().SetGameLevel((int)gameTime / 30 + 1);

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
        if (!_Running) return;

        audioSource.PlayOneShot(explo);
        StartCoroutine(WaitFoeExplo());
    }

    IEnumerator WaitFoeExplo()
    {
        yield return new WaitForSecondsRealtime(audioLength);
    }

    public void PlayerHurt()
    {
        hp--;
        hearts[hp].SetActive(true);
    }

    public void StartGame()
    {
        if (!_Running)  SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (_Running)
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
    }
    public void PlayGame()
    {
        if (_Running)
        {
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }
    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("MaxScore")) PlayerPrefs.SetInt("MaxScore", score);
        SceneManager.LoadScene("EndScene");
    }
    public void GameReset()
    {
        Time.timeScale = 1f;

        score = 0;
        gameTime = 0;

        if (!PlayerPrefs.HasKey("MaxScore")) maxScore = 0;
        else maxScore = PlayerPrefs.GetInt("MaxScore");

        ScoreText.text = $"{score}";
        MaxText.text = $"{maxScore}";
        gameTime = 0f;

        foreach (var h in hearts)
        {
            h.SetActive(false);
        }

        hp = hearts.Length;
    }

    public void AddPoint(int point)
    {
        score++;
        ScoreText.text = $"{score}";
        if (score >= maxScore)
        {
            MaxText.text = $"{score}";
        }
    }
}
