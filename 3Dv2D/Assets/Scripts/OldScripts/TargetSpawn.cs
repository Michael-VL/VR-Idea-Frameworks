using UnityEngine;
using System.Collections;

public class TargetSpawn : MonoBehaviour {

    public int numTargets;
    int curNumTargets;
    bool spawnComplete;

    public GameObject target;
    public Transform[] spawnLoc;
    int ranLoc;

    void Start()
    {
        curNumTargets = 0;
        spawnLoc = GetComponentsInChildren<Transform>();
    }
    void Update()
    { 
        if (curNumTargets < numTargets && !spawnComplete)
        {
            ranLoc = Random.Range(0, spawnLoc.Length);

            Instantiate(target, spawnLoc[ranLoc].position, Quaternion.identity);
            curNumTargets += 1;

            if(curNumTargets == numTargets)
            {
                spawnComplete = true;
            }
        }
    }
}
