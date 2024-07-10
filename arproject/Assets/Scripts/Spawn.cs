using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject[] prefab;
    public GameObject BoomPrefab;
    public AudioClip spawnClip;
    public AudioClip boomClip;
    public GameObject camera;
    public Text timeTxt;

    AudioSource audio;
    float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        audio  = GetComponent<AudioSource>();

        StartCoroutine(WaitAndSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        timeTxt.text = gameTime / 60 + " : " + gameTime % 60;

        if(gameTime == 30f)
        {

        }
    }
    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            int spawnCnt = 0;
            float maxSpawn = 0f;
            float minSpawn = 0f;

            if (gameTime < 30f)
            {
                spawnCnt = 3;
                minSpawn = 2f;
                maxSpawn = 4f;
            }
            else
            {
                spawnCnt = 4;
                minSpawn = 1f;
                maxSpawn = 3f;
            }

            float waitTime = Random.Range(minSpawn, maxSpawn);
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < spawnCnt; i++)
            {
                bool isSpawnBoom = false;
                int iPrefab = Random.Range(0, prefab.Length);
                int isBoom = Random.Range(0, 10);
                float xpos = Random.Range(-3f, 3f);
                GameObject obj;

                if (isBoom < 7)
                {
                    obj = Instantiate(prefab[iPrefab], new Vector3(xpos, -2f, 2.5f), Quaternion.identity);
                    obj.transform.LookAt(camera.transform);
                    Destroy(obj, 5f);
                }
                else
                {
                    obj = Instantiate(BoomPrefab, new Vector3(xpos, -2f, 2.5f), Quaternion.identity);
                    obj.transform.LookAt(camera.transform);
                    isSpawnBoom = true;

                    Destroy(obj, 10f);
                }

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                rb.AddForce(Vector3.up * Random.Range(4.0f, 10.0f), ForceMode.VelocityChange);

                if (isSpawnBoom) audio.PlayOneShot(boomClip);
            }
            
            
            audio.PlayOneShot(spawnClip);
        }
    }

    public void Reset()
    {
        gameTime = 0f;
    }
}
