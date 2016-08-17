using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

    public float speed;
    public float raySpeed;
    float startTime;
    public float destructTime;
    public float timeUntil;
	
	void Start()
    {
        startTime = Time.time;
    }
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raySpeed))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            Debug.DrawRay(transform.position, ray.direction);
            //Vector3 relfectDir = Vector3.Reflect(ray.direction, hit.normal);

            //transform.rotation = Quaternion.LookRotation(relfectDir);
        }
        float curTime = Time.realtimeSinceStartup - startTime;
        timeUntil = destructTime - curTime;
        if(curTime >= destructTime)
        {
            Destroy(transform.gameObject);
        }
	}
}
