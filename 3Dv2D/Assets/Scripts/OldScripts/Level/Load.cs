using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "PickUp")
        {
            switch(col.name)
            {
                case "Snow Scene":
                    Application.LoadLevel(1);
                    break;
                case "Bowling":
                    Application.LoadLevel("Bowling");
                    break;
                case "Happy":
                    Application.LoadLevel("Happy");
                    break;
                case "UFOMO":
                    Application.LoadLevel("UFOMO");
                    break;
                case "Lightsaber training":
                    Application.LoadLevel("Lightsaber training");
                    break;
                case "Ethans game thing":
                    Application.LoadLevel("Ethans game thing");
                    break;
                case "Sci":
                    Application.LoadLevel("Sci");
                    break;
            }
        }
        
    }
}
