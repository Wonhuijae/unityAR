using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject gun;
    public GameObject effect;
    public GameObject camera;
    public Text ScoreText;

    AudioSource audio;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        score = 0;
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
            
        }

        //gun.GetComponent<Animator>().SetBool("isShooted", false);
    }

    public void MobKill()
    {
        score++;
        ScoreText.text = "Score : " + score;
    }
}
