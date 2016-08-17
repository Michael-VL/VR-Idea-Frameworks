using UnityEngine;
using System.Collections;

public class HappyAI : MonoBehaviour {

    public bool dead;
    public Component[] rbs;

	void Update()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        if (dead == true)
        {
            foreach(Rigidbody rigidbody in rbs)
            {
                rigidbody.isKinematic = false;
            }
        }
        else
        {
            foreach(Rigidbody rigidbody in rbs)
            {
                rigidbody.isKinematic = true;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Weapon")
        {
            dead = true;
            Destroy(col.gameObject, 20);
        }
    }
}
