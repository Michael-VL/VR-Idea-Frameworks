using UnityEngine;
using System.Collections;

public class basicDestoryTarget : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Destroy(transform.gameObject);
    }
}
