using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    public Transform player;

    void Update()
    {
        transform.LookAt(player);
    }
}
