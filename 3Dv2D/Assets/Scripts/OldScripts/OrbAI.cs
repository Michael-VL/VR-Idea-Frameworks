using UnityEngine;
using System.Collections;

public class OrbAI : MonoBehaviour {

    public GameObject Glow;
    public float angle;
    public float speed;

    public GameObject target;

    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        Glow.transform.Rotate(angle * Time.deltaTime, angle * Time.deltaTime, angle * Time.deltaTime);
        agent.destination = target.transform.position;

        agent.speed = speed;
	}
}
