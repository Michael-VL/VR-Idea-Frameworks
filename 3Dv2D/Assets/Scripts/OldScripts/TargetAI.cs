using UnityEngine;
using System.Collections;

public class TargetAI : MonoBehaviour {

    public float maxHeight;
    public float minHeight;

    public float maxDist;
    public float minDist;

    public float maxDistZ;
    public float minDistZ;

    float genHeight;

    Vector3 newPos;

    float genDist;
    float genDistZ;

    bool genDone;
    bool lerpDone;

    void Start()
    {
        genDone = false;
    }
    void Update()
    {
        if (!genDone)
        {
            genHeight = Random.Range(minHeight, maxHeight);
            genDist = Random.Range(minDist, maxDist);
            genDistZ = Random.Range(minDistZ, maxDistZ);

            newPos = new Vector3(genDist, genHeight, genDistZ);
            genDone = true;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);

        if(transform.position == newPos)
        {
            lerpDone = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy" && lerpDone)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Time.deltaTime);
        }
        if(col.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Level")
        {
            transform.position += transform.up * 2;
        }
    }
}
