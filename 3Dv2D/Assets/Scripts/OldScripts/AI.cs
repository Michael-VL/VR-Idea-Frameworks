using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    //Get type of enemy
    public bool runner;
    public bool shooter;

    public GameObject Weapon;
    public GameObject Bullet;
    public bool carryingWeapon;
    bool doneCooling;

    //public float stoppingDist;
    public float randomStoppingThreshold;
    public Transform target;

    public bool targetSighted;
    bool stopped;

    float currentDist;
    float currentTimeLog;
    bool logged = false;

    public GameObject gunEnd;
    //Set components
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        //Get components
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        gunEnd = Weapon.transform.Find("gunEnd").gameObject;
        doneCooling = true;

    }

	void Update()
    {
        //Find the player
        currentDist = Vector3.Distance(transform.position, target.position);
        //if want, go to player
        if (!targetSighted)
        {
            agent.destination = target.position;
            anim.SetBool("Stopped", false);
            anim.SetBool("Walk", false);
        }
        
        if(targetSighted && shooter)
        {
            //stop running animation
            //anim.SetBool("Stopped", true);
            //anim.SetBool("Walk", false);
            //set ik position to player
            var randNum = Random.Range(0, 1);
            if(randNum >= randomStoppingThreshold)
            {
                //Debug.Log("Stop");
                //enemy stops
                agent.destination = transform.position;
                stopped = true;
                //raise weapon
                anim.SetBool("Stopped", true);
                //play attack 
                Shoot();
            }
            else
            {
                Debug.Log("Random Delay");
                Delayed(2f, false);
                //StartCoroutine("DelayRandom", 2f);
            }
        }
        //else keep running
            //play attack animation
        else if(shooter)
        {
            if(currentDist <= 5)
            {
                agent.destination = transform.position;
                stopped = true;
                anim.SetBool("Stopped", true);
                //play attack animation
                Shoot();
            }
        }
        else if (runner)
        {
            if (currentDist <= .5)
            {
                agent.destination = transform.position;
                stopped = true;
                anim.SetBool("Stopped", true);
                //play attack animation
                //anim.SetBool("",true);
            }
        }
        
        if(!carryingWeapon)
        {
            shooter = false;
            runner = true;
            //if no weapon find closest weapon
            //go to weapon, stop, crouch animation, lerp ik to gun
        }

        RaycastHit hit;
        Vector3 rayDirection = target.transform.position - transform.position;
        
        Debug.DrawRay(transform.position, rayDirection * 20, Color.green);
        if (Physics.Raycast(transform.position, rayDirection, out hit, 20))
        {
            if (hit.collider.tag == "Player")
            {
                //Debug.Log("See");
                targetSighted = true;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //if killed, ragdoll, drop weapon
        if (col.gameObject.tag == "Bullet")
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
        }
        if(col.gameObject.tag == "Throwable")
        {
            var weaponRb = Weapon.GetComponent<Rigidbody>();
            //play injured recoil
            Weapon.transform.parent = null;
            weaponRb.isKinematic = false;

            carryingWeapon = false;

            //Manager.AddGun(Weapon);
            //Manager.NearestGun(transform.gameObject);
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if(stopped)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, target.position + Vector3.up);
            transform.LookAt(target, Vector3.up);
            Shoot();
        }
    }

    void Shoot()
    {
        if(doneCooling)
        {
            Debug.Log("shoot");
            anim.SetTrigger("Fire");

            Delayed(.01f, true);
            //StartCoroutine("DelayRandom", .01f);
            //Instantiate(Bullet, gunEnd.transform.position, gunEnd.transform.rotation);
            //doneCooling = false;
            
            //StartCoroutine("DelayRandom", 2f);
        }
    }

    void Shot()
    {
        Instantiate(Bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        doneCooling = false;

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Detected");
    //    if (col.gameObject.tag == "Player")
    //    {
            
    //    } 
    //}

    //IEnumerator DelayRandom(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    doneCooling = true;
    //}

    void Delayed(float time, bool shot)
    {

        if(!logged)
        {
            Debug.Log("Looping");
            currentTimeLog = Time.realtimeSinceStartup;
            logged = true;
        }
        
        if(Time.realtimeSinceStartup < (currentTimeLog + time))
        {
            return;
        }
        if(shot)
        {
            Shot();
            Debug.Log("continuing");
            Delayed(2f, false);
        }
        else
        {
            doneCooling = true;
        }

        logged = false;

    }
}
