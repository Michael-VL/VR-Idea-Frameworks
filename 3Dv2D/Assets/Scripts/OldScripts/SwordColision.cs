using UnityEngine;
using System.Collections;

public class SwordColision : MonoBehaviour {

    public AudioClip sound;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enemy Hit");
        if (col.gameObject.tag == "Enemy")
        {
            source.Play(1);
            col.gameObject.tag = "PickUp";
        }
    }



    // when the sword colides with an baddy it plays the hurt sound
        // also hurts the baddy
    // when it collides with any other object is plays the bang sound
}
