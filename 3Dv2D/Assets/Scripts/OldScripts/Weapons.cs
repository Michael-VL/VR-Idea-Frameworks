using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    public GameObject controllerL;
    public GameObject controllerR;

    public GameObject[] weapons;

    public static bool stateL;
    public static bool stateR;

    void Start()
    {
        stateL = false;
        stateR = false;
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("detect controller");
        //Debug.Log(stateL + ":" + stateR);
        if (col.gameObject.tag == "ControllerL" && stateL == false)
        {
            stateL = true;
            var randPick = Random.Range(0, weapons.Length);
            //Debug.Log("new weaponL");
            GameObject newWeaponL = Instantiate(weapons[randPick], controllerL.transform.position, Quaternion.identity) as GameObject;
            newWeaponL.name = "L";
            newWeaponL.transform.parent = controllerL.transform;
            newWeaponL.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        else if (col.gameObject.tag == "ControllerR" && stateR == false)
        {
            stateR = true;
            var randPick = Random.Range(0, weapons.Length);
            //Debug.Log("new weaponR");
            GameObject newWeaponR = Instantiate(weapons[randPick], controllerR.transform.position, Quaternion.identity) as GameObject;
            newWeaponR.name = "R";
            newWeaponR.transform.parent = controllerR.transform;
            newWeaponR.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
