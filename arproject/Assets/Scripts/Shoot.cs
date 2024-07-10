using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject gun;
    public GameObject effect;
    public GameObject camera;
    public GameObject Offset;
    public Text ScoreText;
    public Text MaxText;

    AudioSource audio;
    int score;
    int maxScore;
    int hp = 3;
    private GameObject boom;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        score = 0;

        if (!PlayerPrefs.HasKey("MaxScore")) maxScore = 0;
        else maxScore = PlayerPrefs.GetInt("MaxScore");

        ScoreText.text = $"{score}";
        MaxText.text = $"{maxScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        audio.Play();
        gun.GetComponent<Animator>().SetBool("isShooted", true);

        RaycastHit hit;
        if( Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) )
        {
            if (hit.transform.tag == "Enemy") 
            {
                Destroy(hit.transform.gameObject);
                Instantiate(effect, hit.point, Quaternion.identity);
            }
            else if(hit.transform.tag == "Boom")
            {
                boom = hit.transform.gameObject;
                boom.GetComponent<Rigidbody>().useGravity = false;
                boom.transform.position = Offset.transform.position;
                boom.transform.GetComponent<Animator>().SetTrigger("damage");;
            }
        }
        //gun.GetComponent<Animator>().SetBool("isShooted", false);
    }

    public void MobKill()
    {
        score++;
        ScoreText.text = $"{score}";
        if (score >= maxScore)
        {
            MaxText.text = $"{score}";
        }
    }

    public void BoomKill()
    {
        hp--;
        if (hp == 0) GameManager.instance.GetComponent<GameManager>().GameOver(score);
    }

    public void Reset()
    {
        score = 0;
    }
}
