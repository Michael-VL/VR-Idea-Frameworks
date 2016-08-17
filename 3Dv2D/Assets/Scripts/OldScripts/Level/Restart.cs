using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

    public float pushDelay;

    bool restart;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
	void Update()
    {
       
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "PickUp")
        {
            anim.SetTrigger("Push");
            StartCoroutine("pushed", pushDelay);
            
        }
    }
    IEnumerator pushed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.LoadLevel(1);
    }
}
