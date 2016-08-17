using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shatter : MonoBehaviour {

    public List<GameObject> stuff;

    void OnTriggerEnter(Collider col)
    {
        if(!stuff.Contains(col.gameObject) && col.tag != "Floor")
        {
            stuff.Add(col.gameObject);
        }

        foreach (GameObject stuffthings in stuff)
        {
            stuffthings.transform.parent = null;
            stuffthings.AddComponent<Rigidbody>();
        }
    }
}
