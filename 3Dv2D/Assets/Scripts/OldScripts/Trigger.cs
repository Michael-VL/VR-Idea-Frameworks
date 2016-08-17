using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{

    public GameObject script;
    public bool left;
    public bool right;

    void Start()
    {
        left = false;
        right = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (left)
            {
                script.SendMessage("Avoid", "left");
                left = false;
            }
            else if (right)
            {
                script.SendMessage("Avoid", "right");
            }
        }
    }
}