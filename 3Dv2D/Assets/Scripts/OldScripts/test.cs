using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    //public GameObject target;
    public GameObject bullet;
    public GameObject gunEnd;

    bool wait = true;

    void Update()
    {
        //float length = 10.0f;
        //RaycastHit hit;
        //Vector3 rayDirection = target.transform.position - transform.position;

        //Debug.DrawRay(transform.position, rayDirection * length, Color.green);
        //if (Physics.Raycast(transform.position, rayDirection, out hit, length))
        //{
        //    if (hit.collider.tag == "PickUp")
        //    {
        //        Debug.Log("Hello");
        //    }
        //}

        Debug.DrawRay(gunEnd.transform.position, gunEnd.transform.forward * 20, Color.green);
        if(wait)
        {
            StartCoroutine("TestShoot");
        }

    }
    IEnumerator TestShoot()
    {
        wait = false;
        yield return new WaitForSeconds(2f);

        Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        wait = true;
    }
}
