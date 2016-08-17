using UnityEngine;
using System.Collections;

public class BallPit : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Bowling")
        {
            Destroy(col);
        }
    }
}
