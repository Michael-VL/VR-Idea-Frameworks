using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Application.LoadLevel(0);
        }
    }
}
