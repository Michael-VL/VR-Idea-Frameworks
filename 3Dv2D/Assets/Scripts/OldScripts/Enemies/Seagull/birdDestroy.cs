using UnityEngine;
using System.Collections;

public class birdDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Died");
        if (col.gameObject.tag == "Bird")
        {
            //Debug.Log("Died");
            Population.death();
            Destroy(col.gameObject);
        }
    }
}
