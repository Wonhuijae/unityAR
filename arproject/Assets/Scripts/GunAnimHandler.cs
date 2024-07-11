using UnityEngine;
public class GunAnimHandler : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFireExit()
    {
        anim.SetBool("isShooted", false);
    }

}
