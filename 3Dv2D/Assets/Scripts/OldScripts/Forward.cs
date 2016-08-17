using UnityEngine;
using System.Collections;

public class Forward : MonoBehaviour {

    public float speed;
    Rigidbody rig;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rig.AddRelativeForce(Vector3.forward * speed);
    }
}
