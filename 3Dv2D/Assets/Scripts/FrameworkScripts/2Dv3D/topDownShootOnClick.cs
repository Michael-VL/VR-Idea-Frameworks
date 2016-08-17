using UnityEngine;
using System.Collections;

public class topDownShootOnClick : MonoBehaviour {

    public GameObject bullet;
    public Transform target;
    public float bulletSpeed;

    void FixedUpdate()
    {
        var directionVector = target.position - transform.position;
        if(Input.GetMouseButtonDown(0))
        {
            var instanceBullets = Instantiate(bullet, transform.position, Quaternion.LookRotation(directionVector)) as GameObject;

            var bulletRig = instanceBullets.GetComponent<Rigidbody>();
            bulletRig.AddForce(transform.forward * bulletSpeed);
        }
    }
}
