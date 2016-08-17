using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour
{
    //declare variables
    public bool runner;
    public bool shooter;

    public GameObject player;
    public GameObject bullet;

    public float rayLength;
    public float shootingRange;
    public float avoidSpeed;
    float distance;

    public GameObject gunEnd;
    GameObject weapon;
    GameObject nearestWeapon;

    bool dead;
    bool visible;
    bool equipped;

    NavMeshAgent agent;
    Animator anim;
    GameObject hand;

    void Start()
    {
        //get components
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hand = transform.Find("Hand_R").gameObject;
    }

    void Update()
    {
        //check current state
        if (!dead)
        {
            //check visibility
            RaycastHit hit;
            Vector3 rayDirection = player.transform.position - transform.position;

            Debug.DrawRay(transform.position, rayDirection * rayLength, Color.green);
            if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength))
            {
                if (hit.collider.tag == "Player")
                {
                    visible = true;
                }
            }
            //check weapon
            //if (!equipped)
            //{
            //    Weapon();
            //}

            if (visible)
            {
                distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= shootingRange)
                {
                    Shoot();
                }
            }
        }
        else
        {
            Dead();
        }
    }

    void Shoot()
    {
        //start animation
        //aim and shoot
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position + Vector3.up);
        Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
    }

    void Avoid(string side)
    {
        if (side == "left")
        {
            transform.localPosition += transform.right * avoidSpeed;
        }
        else if (side == "right")
        {
            transform.localPosition -= transform.right * avoidSpeed;
        }
    }

    //takes destination
    void Destination(Transform trans)
    {
        //set destination
        agent.destination = trans.position;
    }

    //void Weapon()
    //{
    //    //find nearest weapon
    //    Manager.NearestGun(transform.gameObject);
    //    nearestWeapon = Manager.nearestGun;
    //    //set destination
    //    Destination(nearestWeapon.transform);
    //    //Destination();
    //    //pick up weapon
    //    //set shooting end
    //    gunEnd = nearestWeapon.transform.Find("gunEnd").gameObject;
    //    var gunHandle = nearestWeapon.transform.Find("Handle").gameObject;
    //    hand.transform.position = gunHandle.transform.position;
    //    nearestWeapon.transform.parent = hand.transform;
    //}

    //void Hurt()
    //{
    //    //release weapon
    //    nearestWeapon.transform.parent = null;
    //}

    void Dead()
    {
        WaveSpawner.counter("subract");
    }
}
