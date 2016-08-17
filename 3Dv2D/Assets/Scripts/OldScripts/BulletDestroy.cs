using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

    public static bool death;
    bool deathsub;

    void Update()
    {
        death = deathsub;
    }
	void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            deathsub = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
