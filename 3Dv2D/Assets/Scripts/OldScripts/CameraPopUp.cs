using UnityEngine;
using System.Collections;

public class CameraPopUp : MonoBehaviour {

    public GameObject[] array;
    bool lookedOther;

    void Start()
    {
        foreach(GameObject game in array)
        {
            game.SetActive(false);
        }
    }
    public void Appear()
    {
        var randomNumber = Random.Range(0, array.Length);


    }

    void LookedOtherWay()
    {

    }
}
