using UnityEngine;
using System.Collections;

public class teleportUsingLaser : MonoBehaviour {

    LineRenderer laserPointer;
    SteamVR_TrackedObject trackedobj;

    public GameObject laserStart;
    public GameObject body;

    public float maxDistance;

    Vector3 targetTeleportation;

    void Start()
    {
        laserPointer = GetComponent<LineRenderer>();
        trackedobj = GetComponent<SteamVR_TrackedObject>();

        laserPointer.enabled = false;
        laserPointer.SetPosition(0, laserStart.transform.position);
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedobj.index);
        
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            targetTeleportation = callLaserPointer(true);

            if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
            {
                body.transform.position = targetTeleportation;
            }
        }
        else if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            callLaserPointer(false);
        }
    }

    Vector3 callLaserPointer(bool laserTriggered)
    {
        if(true)
        {
            Ray ray = new Ray(laserStart.transform.position, laserStart.transform.forward);

            RaycastHit hit;
            laserPointer.enabled = true;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                laserPointer.SetPosition(1, hit.point);
                laserPointer.SetColors(Color.green, Color.green);
                return hit.transform.position;
            }
            else
            {
                laserPointer.SetPosition(1, ray.GetPoint(maxDistance));
                laserPointer.SetColors(Color.red, Color.red);
                return body.transform.position;
            }
        }
        else
        {
            laserPointer.enabled = false;
        }
    }
}
