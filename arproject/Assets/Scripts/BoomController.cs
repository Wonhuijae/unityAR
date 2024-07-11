using UnityEngine;

public class BoomController : MonoBehaviour
{
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamageAnimationEnd()
    {
        anim.SetTrigger("attack01");
    }


    public void OnAttackAnimationEnd()
    {
        GameManager.instance.GetComponent<GameManager>().Explosion();
        Destroy(gameObject);
    }
}
