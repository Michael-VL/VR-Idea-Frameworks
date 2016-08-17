using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {

    public float speed;
    public GameObject stick;
    Rigidbody stickRig;
    Rigidbody rig;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        stickRig = stick.GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.position += transform.forward * -speed;
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "PickUp")
        {
            stickRig.useGravity = true;
            stickRig.isKinematic = false;
            stick.transform.parent = null;
            stick.tag = "PickUp";

            rig.useGravity = true;
            rig.isKinematic = false;
            Destroy(transform.gameObject, 5);
        }
    }
}
