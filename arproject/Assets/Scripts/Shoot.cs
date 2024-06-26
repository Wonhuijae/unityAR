using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gun;
    public GameObject camera;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        audio.Play();
        Debug.Log("ÃÑ ½úÀ½");
        gun.GetComponent<Animator>().SetBool("isShooted", true);

        RaycastHit hit;
        if( Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) )
        {
            Debug.Log("Ãæµ¹");
            if (hit.transform.tag == "Bear") 
            {
                Destroy(hit.transform.gameObject);
                Instantiate(gun, hit.point, Quaternion.LookRotation(hit.normal));
                Debug.Log("°õ");
            }
            
        }

        gun.GetComponent<Animator>().SetBool("isShooted", false);
    }
}
