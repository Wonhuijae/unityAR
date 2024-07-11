using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gun;
    public GameObject effect;
    public GameObject camera;
    public GameObject Offset;

    AudioSource audio;
   
    private GameObject boom;

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
        gun.GetComponent<Animator>().SetBool("isShooted", true);

        RaycastHit hit;
        if( Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) )
        {
            if (hit.transform.tag == "Enemy") 
            {
                Destroy(hit.transform.gameObject);
                Instantiate(effect, hit.point, Quaternion.identity);
                GameManager.instance.GetComponent<GameManager>().AddPoint(1);
            }
            else if(hit.transform.tag == "Boom")
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                boom = hit.transform.gameObject;

                rb.velocity = Vector3.zero;
                boom.transform.position = Offset.transform.position;
                boom.transform.rotation.SetLookRotation(camera.transform.position);
                boom.transform.GetComponent<Animator>().SetTrigger("damage");;
            }
        }
        //gun.GetComponent<Animator>().SetBool("isShooted", false);
    }

    public void BoomKill()
    {
        GameManager.instance.GetComponent<GameManager>().GameOver();
    }
}