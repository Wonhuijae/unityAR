using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public Transform[] pos;
    public GameObject[] prefab;
    public AudioClip spawnClip;
    public GameObject camera;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio  = GetComponent<AudioSource>();

        StartCoroutine(WaitAndSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            float waitTime = Random.Range(2.0f, 4.0f);
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < 3; i++)
            {
                int iPrefab = Random.Range(0, prefab.Length);
                int iPos = Random.Range(0, pos.Length);

                GameObject obj = Instantiate(prefab[iPrefab], pos[iPos].position, Quaternion.identity);
                obj.transform.LookAt(camera.transform);

                Destroy(obj, 5f);

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                rb.AddForce(Vector3.up * Random.Range(4.0f, 10.0f), ForceMode.VelocityChange);
            }

            audio.PlayOneShot(spawnClip);
        }
    }
}
