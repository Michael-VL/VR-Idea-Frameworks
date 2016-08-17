using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyingAI : MonoBehaviour {

    public GameObject getFromArray;

    public List<Transform> wayPoints;
    public float speed;
    public GeneralShooting shoot;
    public Transform target;

    bool occupied = true;
    bool pickedLoc = false;
    Transform lastOccupied;

    bool moving = false;
    Transform storeLoc;

    bool checkUntilThere;

    int randNum;

    public float currentHealth;

    Rigidbody rig;

    public GameObject ParticleSystem;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        foreach(Transform point in getFromArray.transform)
        {
            wayPoints.Add(point);
        }
    }

    //check state
    void Update()
    {
        transform.LookAt(target);

        if (!pickedLoc)
        {
            Pick();
        }
        if(moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, storeLoc.position, Time.deltaTime * speed);
        }

        if(checkUntilThere)
        {
            Check();
        }
    }

    //select waypoint
    void Pick()
    {
        while (occupied)
        {
            //Debug.Log("Picking");
            randNum = Random.Range(0, wayPoints.Count);

            if (wayPoints[randNum].tag != "Occupied")
            {
                occupied = false;
                pickedLoc = true;
                wayPoints[randNum].tag = "Occupied";
                Move(wayPoints[randNum]);
                //Debug.Log(randNum);
            }
        } 

    }

    //move to waypoint
    void Move(Transform moveTo)
    {
        //Debug.Log("moving");
        storeLoc = moveTo;
        moving = true;

        checkUntilThere = true;

    }

    void Check()
    {
        if (transform.position == wayPoints[randNum].position)
        {
            moving = false;
            storeLoc = null;
            Attack(wayPoints[randNum]);
            pickedLoc = false;
            wayPoints[randNum].tag = "UnOccupied";
            occupied = true;
            Pick();
            //Debug.Log("should pick new location");

            checkUntilThere = true;
        }
    }

    //attacking player
    void Attack(Transform moveTo)
    {
        transform.LookAt(target);

        //var rotationToTarget = Quaternion.LookRotation(target.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, 99 * Time.deltaTime);

        shoot.Shoot();
        pickedLoc = false;
    }

    //being hit
    void OnTriggerEnter(Collider col)
    {
        rig.AddForce(0, 0, -5);
        if(col.gameObject.tag == "Bullet")
        {
            Health(5f);
        }
    }

    void Health(float damage)
    {
        if(currentHealth != 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Population.death();
            ParticleSystem.SetActive(true);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,1) , speed * Time.deltaTime);
            if(transform.localScale == new Vector3(0,0,1))
            {
                Destroy(gameObject);
            }
            //Destroy(transform.gameObject);
        }
    }
}
