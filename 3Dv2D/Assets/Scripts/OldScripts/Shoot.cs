using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject weaponArea;

    public Transform barrelEnd;
    public float ammo;
    public GameObject bullet;

    public bool isSemiAuto;
    bool fireAuto;
    public float fireRateTiming;
    bool coolDownComplete = true;

    bool throwable;

    public SteamVR_TrackedObject trackedObj;

    bool fireable;

    Rigidbody rig;
    FixedJoint joint;

    //Disappear disappearScript;

    public string parentName;

    public Shoot shootScript;

    void Start()
    {
        //disappearScript = GetComponent<Disappear>();
        weaponArea = GameObject.FindGameObjectWithTag("newWeaponArea");
        coolDownComplete = true;
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
        joint = GetComponentInParent<FixedJoint>();

        throwable = false;
        rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;

        parentName = transform.parent.name;

        shootScript = GetComponent<Shoot>();
    }
	void Update()
    {
        if(fireAuto)
        {
            FireGun();
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (ammo > 0)
        {
            fireable = true;
        }
        else if(ammo <= 0)
        {
            //Debug.Log("out of ammo");
            fireable = false;
            fireAuto = false;
            //OutAmmo();
        }

        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip) && fireable == false)
        {
            joint.connectedBody = rig;
            rig.isKinematic = false;
            //transform.parent = null;
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip) && fireable == false)
        {
            OutAmmo();
            rig.useGravity = true;
            rig.isKinematic = false;
            joint.connectedBody = null;
            transform.parent = null;

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
            
            rig.maxAngularVelocity = rig.angularVelocity.magnitude;
            Destroy(shootScript);
            //disappearScript.Change();
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && fireable)
        {
            if(!isSemiAuto)
            {
                FireGun();
            }
            else
            {
                fireAuto = true;
            }
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(fireAuto)
            {
                fireAuto = false;
            }
        }
    }

    void FireGun()
    {
            if(fireAuto)
            {
                    if(coolDownComplete)
                    {
                        Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                        CountAmmo();
                        coolDownComplete = false;
                        StartCoroutine("FastFire");
                    }
            }
            else
            {
                CountAmmo();
                Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
            }
    }

    void OutAmmo()
    {
        //Debug.Log("reset state");
        if(transform.parent.gameObject.name == "L")
        {
            Weapons.stateL = false;
        }
        else if(transform.parent.gameObject.name == "R")
        {
            Weapons.stateR = false;
        }
        //Debug.Log(Weapons.stateL + " : " + Weapons.stateR);
    }

    void CountAmmo()
    {
        ammo -= 1f;
    }

    IEnumerator FastFire()
    {
        yield return new WaitForSeconds(fireRateTiming);
        coolDownComplete = true;
    }
}
