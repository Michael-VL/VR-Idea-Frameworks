using UnityEngine;
using System.Collections;

public class hurt : MonoBehaviour {

    AudioSource sounds;
    public AudioClip clip;
    public static bool play;
    public bool plays;
    
    void Start()
    {
        sounds = GetComponent<AudioSource>();
    }
    void Update()
    {
        plays = play;

        if(plays)
        {
            sounds.Play(0);
            play = false;
            plays = false;
        }
    }
}
