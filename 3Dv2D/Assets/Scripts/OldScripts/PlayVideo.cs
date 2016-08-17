using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {

    MovieTexture movTexture;
    public float timeAfter;
    float time;

	void Start () {
        timeAfter = Random.Range(0, 10);
        movTexture = GetComponent<MovieTexture>();
        time = Time.time;
    }
    
    void Update()
    {
        if(time + timeAfter == Time.time)
        {
            movTexture.Play();
        }
    }
}
