using UnityEngine;
using System.Collections;

public class pinReset : MonoBehaviour {

    public GameObject pins;
    public Transform position;
    SteamVR_TrackedObject trackobj;

    bool active;
    GameObject spawn;

    void Awake()
    {
        trackobj = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackobj.index);
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip) && active == false)
        {
            spawn = Instantiate(pins, position.position, Quaternion.identity) as GameObject;
            active = true;
        }
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip) && active == true)
        {
            Destroy(spawn);
            spawn = Instantiate(pins, position.position, Quaternion.identity) as GameObject;
            active = true;
        }
    }
}
