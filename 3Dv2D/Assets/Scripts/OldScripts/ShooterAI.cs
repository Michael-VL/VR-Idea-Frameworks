using UnityEngine;
using System.Collections;

public class ShooterAI : MonoBehaviour {

    public Transform target;
    public float health;
    public float damage;

    bool dead;
    bool isVisible;
    bool shooting;
    bool moving;

    public float maxAllottedDistanceToPlayer;
    float distanceToPlayer;

    NavMeshAgent agent;
    Rigidbody[] ragRigs;
    ShooterAIAttack shootScript;
    Animator anim;

    void Start()
    {
        shootScript = GetComponent<ShooterAIAttack>();
        agent = GetComponent<NavMeshAgent>();
        ragRigs = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();

        foreach (Rigidbody rig in ragRigs)
        {
            rig.isKinematic = false;
        }
    }

    void Update()
    {
        transform.LookAt(target);
        if (!dead)
        {
            
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if(distanceToPlayer <= maxAllottedDistanceToPlayer)
            {
                agent.SetDestination(transform.position);
                moving = false;
                anim.SetBool("Stopped", true);
                anim.SetBool("Walk", false);
            }
            if (!isVisible)
            {
                //move to player until seen
                Move(target);
                //check line of sight
                StartCoroutine("LOS");   
            }

            if(isVisible)
            {
                //Debug.Log("set destination visible should stop" + Time.time);
                agent.SetDestination(transform.position);
                moving = false;
                anim.SetBool("Stopped", true);
                anim.SetBool("Walk", false);
            }

            if(!moving)
            {
                shootScript.ShootAnim();
            }

            //if(distanceToPlayer < maxAllottedDistanceToPlayer)
            //{
            //    agent.SetDestination(transform.position);
            //    moving = false;
            //    isVisible = true;
            //    anim.SetBool("Stopped", true);
            //    anim.SetBool("Walk", false);
            //}
        }
    }

    //move towards target
    void Move(Transform toTarget)
    {
        //set destination
        anim.SetBool("Stopped", false);
        anim.SetBool("Walk", false);

        agent.SetDestination(toTarget.position);
        moving = true;
    }

    //avoid bullet
    void Avoid(bool left)
    {
        if (!shooting)
        {
            moving = true;
            if (left)
            {
                transform.localPosition += transform.right * 2;
                moving = false;
            }
            else if (!left)
            {
                transform.localPosition -= transform.right * 2;
                moving = false;
            }
        }
    }

    //die
    void Death()
    {
        //StopCoroutine("LOS");
        //ragdoll
        foreach (Rigidbody rig in ragRigs)
        {
            rig.isKinematic = false;
        }

        WaveSpawner.counter("subtract");
        //disappear
        Destroy(gameObject);
    }

    //line of sight
    IEnumerator LOS()
    {
        //Debug.Log("LOS CHECK");
        yield return new WaitForSeconds(Random.Range(0, 2));

        RaycastHit hit;
        Vector3 rayDirection = target.transform.position - transform.position;

        if(Physics.Raycast(transform.position, rayDirection, out hit, 50))
        {
            if(hit.collider.tag == "Player")
            {
                //if visible 'verbatim'
                //Debug.Log("player detected" + Time.time);
                StartCoroutine("StopAfterRandomTime");
            }
        }
    }

    //stop after a while when target is seen
    IEnumerator StopAfterRandomTime()
    {
        
        yield return new WaitForSeconds(Random.Range(0, 10));
        //.Log("visible should stop" + Time.time);
        isVisible = true;
    }

    void Health()
    {
        health -= damage;
        if(health <= 0)
        {
            dead = true;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Health();
        }
    }
}
