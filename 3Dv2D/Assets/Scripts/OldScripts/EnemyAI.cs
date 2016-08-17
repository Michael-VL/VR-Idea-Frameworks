using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public GameObject player;
    public float distance;
    public float stoppingDistance;

    bool stopped;

    public float speed;

    NavMeshAgent navAgent;
    Animator anim;
    
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        //Move forward towards the player
        navAgent.destination = player.transform.position;
        //At a certain distance, stop
        distance = Vector3.Distance(transform.position, player.transform.position);
        //When stopping, aim and fire the weapon
        if(distance <= stoppingDistance)
        {
            navAgent.destination = transform.position;
            stopped = true;
        }
        //Procede to fire
        if(stopped)
        {
            transform.LookAt(player.transform);
            anim.Play("Raise");
            Shoot();
        }
    }

    void Shoot()
    {
       // Instantiate()
    }
}
