using UnityEngine;
using System.Collections;

public class basicColliderHealth : MonoBehaviour {

    public int health;
    public int damage;

	void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "damage")
        {
            health -= damage;
        }


        if(health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }


}
