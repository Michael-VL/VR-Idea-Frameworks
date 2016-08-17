using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public Light fire;
    public float fuel;
	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "twig")
        {
            fire.intensity += fuel;
            Destroy(col.gameObject);
        }
    }
}
