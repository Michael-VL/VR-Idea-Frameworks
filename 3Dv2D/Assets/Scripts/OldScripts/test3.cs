using UnityEngine;
using System.Collections;

public class test3 : MonoBehaviour {

    public float raySpeed;
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit, Time.deltaTime * raySpeed + .1f))
        {
            //Debug.DrawRay(transform.position, ray.direction, Color.red);
            Vector3 relfectDir = Vector3.Reflect(ray.direction, hit.normal);
            //Debug.DrawRay(hit.transform.position, relfectDir, Color.red);
            transform.rotation = Quaternion.LookRotation(relfectDir);
        }
    }
}
