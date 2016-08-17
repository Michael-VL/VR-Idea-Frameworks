using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Debug.Log("ui interaction logged");
        }
    }
}
