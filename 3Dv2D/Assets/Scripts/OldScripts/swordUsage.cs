using UnityEngine;
using System.Collections;

public class swordUsage : MonoBehaviour {

    public float swordDeflectMax;
    int swordDeflectCounter = 0;
    bool fireable;

    public SteamVR_TrackedObject trackedObj;

    Rigidbody rig;
    FixedJoint joint;
    public GameObject weaponArea;

    void Start()
    {
        joint = GetComponentInParent<FixedJoint>();

        rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();

        weaponArea = GameObject.FindGameObjectWithTag("newWeaponArea");
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet" && swordDeflectCounter < swordDeflectMax)
        {
            swordDeflectCounter += 1;
        }
        else
        {
            fireable = false;
        }
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip) && !fireable)
        {
            rig.isKinematic = false;
            transform.parent = null;
            joint.connectedBody = rig;
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip) && !fireable)
        {
            Destroy(joint);

            if (transform.gameObject.name == "L")
            {
                Weapons.stateL = false;
            }
            else if (transform.gameObject.name == "R")
            {
                Weapons.stateR = false;
            }

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                rig.velocity = origin.TransformVector(device.velocity);
                rig.angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                rig.velocity = device.velocity * 999;
                rig.angularVelocity = device.angularVelocity * 999;
            }

            GetComponent<Rigidbody>().maxAngularVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;
        }
    }
}
