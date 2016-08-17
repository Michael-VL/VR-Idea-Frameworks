using UnityEngine;
using System.Collections;

public class AILogic : MonoBehaviour {

    public bool runner;
    public bool shooter;

    public Transform target;

    public bool targetSighted;
    bool stopped;

    float currentDist;

    public NavMeshAgent agent;
    Animator anim;

    public float distLimit;

    public AIAttack attack;

    bool delayVarience = false;
    bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attack = GetComponent<AIAttack>();

        distLimit = Random.Range(5f, 10f);
    }

    void Update()
    {
        if(isDead)
        {
            Destroy(transform.gameObject);
        }

        currentDist = Vector3.Distance(transform.position, target.position);



        if(currentDist <= distLimit)
        {
            targetSighted = true;
        }

        if (!targetSighted)
        {
            agent.destination = target.position;
            anim.SetBool("Stopped", false);
            anim.SetBool("Walk", false);
        }
        else
        {
            agent.destination = transform.position;
            anim.SetBool("Stopped", true);

        }

        if (targetSighted && shooter && attack.doneCooling == true && currentDist <= distLimit)
        {
            agent.destination = transform.position;
            anim.SetBool("Stopped", true);
            attack.callShoot();

        }
    }

    IEnumerator delay(float time)
    {
        //Debug.Log("delay");
        yield return new WaitForSeconds(time);
        delayVarience = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            isDead = true;
        }

        if(col.gameObject.tag == "Player")
        {
            //Detection
            RaycastHit hit;
            Vector3 rayDirection = target.transform.position - transform.position;

            //Debug.DrawRay(transform.position, rayDirection * 20, Color.green);
            if (Physics.Raycast(transform.position, rayDirection, out hit, 20))
            {
                //Sees player
                if (hit.collider.tag == "Player")
                {
                    targetSighted = true;
                }
                else
                {
                    targetSighted = false;
                }
            }
        }
    }
}
