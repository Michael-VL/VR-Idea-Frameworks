using UnityEngine;
using System.Collections;

public class OnCollisionDisappear : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "3dPlayer")
        {
            Destroy(transform.gameObject);
        }
    }
}
