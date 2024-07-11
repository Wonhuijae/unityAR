using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject[] prefab;
    public GameObject BoomPrefab;
    public AudioClip spawnClip;
    public GameObject camera;
    public Text levelText;
    public GameObject levelPopup;

    AudioSource audio;

    int level = 1;
    int spawnCnt;

    // Start is called before the first frame update
    void Start()
    {
        audio  = GetComponent<AudioSource>();

        StartCoroutine(WaitAndSpawn());
        spawnCnt = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            float maxSpawn = 2f;
            float minSpawn = 4f;

            float waitTime = Random.Range(minSpawn, maxSpawn);
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < spawnCnt; i++)
            {
                int iPrefab = Random.Range(0, prefab.Length);
                int isBoom = Random.Range(0, 10);
                float xpos = Random.Range(-10f, 10f);
                float ypos = Random.Range(-2f, 2f);
                float zpos = Random.Range(-10f, 10f);

                if (-5 < xpos || xpos <= 0)
                {
                    xpos -= 5;
                }
                else if( 0 <= xpos || xpos < 5)
                {
                    xpos += 5;
                }

                if (-5 < zpos || zpos <= 0)
                {
                    zpos -= 5;
                }
                else if (0 <= zpos || zpos < 5)
                {
                    zpos += 5;
                }

                GameObject obj;

                if (isBoom < 7)
                {
                    obj = Instantiate(prefab[iPrefab], new Vector3(xpos, ypos, zpos), Quaternion.identity);
                    obj.transform.LookAt(camera.transform);
                    Destroy(obj, 5f);
                }
                else
                {
                    obj = Instantiate(BoomPrefab, new Vector3(xpos, ypos , zpos), Quaternion.identity);
                    obj.transform.LookAt(camera.transform);
                    Destroy(obj, 10f);
                }

                obj.GetComponent<Enemy>().Move(camera.transform.position);
            }

            audio.PlayOneShot(spawnClip);
        }
    }

    public void SetGameLevel(int _level)
    {
        if (level == _level) return;
        level = _level;
        spawnCnt = level + 2;
        levelPopup.SetActive(true);
        levelText.text = $"Lv. {level}";
    }
}