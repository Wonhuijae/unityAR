using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 target;
    float speed;
    float distance;
    float close;

    AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f, 1f);
        close = Random.Range(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        }

        distance = Vector3.Distance(target, transform.position);
        if (distance == close * 3)
        {
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
            speed = 0;
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip);
            Invoke("Explosion", 3);
        }
    }

    void Explosion()
    {
        GameManager.instance.GetComponent<GameManager>().Explosion();
    }
}
