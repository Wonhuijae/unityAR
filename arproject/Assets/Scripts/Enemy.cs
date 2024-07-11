using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 target;
    float speed;
    float distance;
    float close;
    bool isClose = false;

    AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 1.5f);
        close = Random.Range(speed, speed * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        }

        distance = Vector3.Distance(target, transform.position);
        if (distance <= close && isClose == false)
        {
            isClose = true;
            isClose = true;
            CloseToTarget();
        }
    }

    public void Move(Vector3 _target)
    {
        target = _target;
    }

    void CloseToTarget()
    {
        if (gameObject.tag == "Enemy") return;
        else
        {
            Animator anim = GetComponent<Animator>();
            speed = 0;
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
            anim.SetTrigger("attack01");
        }
    }
}
