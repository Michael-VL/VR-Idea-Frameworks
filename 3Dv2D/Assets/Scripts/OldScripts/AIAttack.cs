using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

    public bool doneCooling = true;

    public Transform target;

    public GameObject Bullet;
    public GameObject gun;
    public GameObject gunEnd;

    Animator anim;

    bool logged = false;
    float savedRealTime;

    bool delayCall;
    float delayTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        //gun = transform.Find("Assault").gameObject;
        //gunEnd = gun.transform.Find("gunEnd").gameObject;
    }

    public void callShoot()
    {
        //Debug.Log("TEST2");
        if (doneCooling)
        {
            //Debug.Log("shoot");
            transform.LookAt(target.transform.position);
            anim.SetTrigger("Fire");

            //Instantiate(Bullet, gunEnd.transform.position, gunEnd.transform.rotation);
            eventShoot();
            
            //StartCoroutine("delaying", 2f);
        }
    }

    public void eventShoot()
    {
        //Debug.Log("exist");
        gunEnd.transform.LookAt(target.position);
        //transform.LookAt(target.position);
        Instantiate(Bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        doneCooling = false;
        delayTime = 2f;
        delayCall = true;
    }

    public void callAttack()
    {
        //attack
    }

    void OnAnimatorIK(int layerIndex)
    {
        //anim.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        //anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        transform.LookAt(target.position);
    }

    IEnumerator delaying(float delay)
    {
        yield return new WaitForSeconds(delay);

        doneCooling = true;
    }
    void delay(float delay)
    {
        if (logged == false)
        {
            savedRealTime = Time.realtimeSinceStartup;
            logged = true;
        }
        if (Time.realtimeSinceStartup <= (savedRealTime + delay))
        {
            return;
        }

        doneCooling = true;
        delayCall = false;
        logged = false;
    }

    void Update()
    {
        if(delayCall)
        {
            delay(delayTime);
        }
    }

}
