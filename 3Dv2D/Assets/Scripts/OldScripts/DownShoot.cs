using UnityEngine;
using System.Collections;

public class DownShoot : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    public bool IsMultiRate;
    public bool multiFiring = false;
    public int multiFireRate;
    int bulletCounter;
    bool canFire = true;
    public float cooldowntime;

    bool coolOrShoot;

    public Transform start;

    public GameObject bullet;

    public float fireSpeed;

    public int ammo;
    public int maxloadedAmmo;

    public int carryAmmo;
    public int maxCarryAmmo;

    bool loading;

    public GameObject gun;
    public GameObject rotation;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (gun != null)
        {
            gun.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        carryAmmo = Mathf.Clamp(carryAmmo, 0, maxCarryAmmo);
        ammo = Mathf.Clamp(ammo, 0, maxloadedAmmo);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && IsMultiRate == true)
        {
            multiFiring = true;
        }

        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            multiFiring = false;
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && ammo > 0 && loading == false || multiFiring && ammo > 0 && loading == false)
        {
            if(canFire)
            {
                coolOrShoot = true;
                StartCoroutine("timed");
                Debug.Log("after");
            }
            else
            {
                coolOrShoot = false;
                StartCoroutine("timed");
                
            }
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {
            loading = true;
        }
        if (loading)
        {
            if (carryAmmo > 0 && ammo < maxloadedAmmo)
            {
                carryAmmo -= 1;
                ammo += 1;
            }
            else
            {
                loading = false;
            }
        }
    }

    void Counter(int shot, int rateLimit, bool rate)
    {
        ammo -= shot;
        if(rate)
        {
            bulletCounter += 1;
            if(bulletCounter >= multiFireRate)
            {
                canFire = false;
            }
            else
            {
                canFire = true;
            }
        }
    }
    
    IEnumerator timed()
    {
        Debug.Log("cooling");
        yield return new WaitForSeconds(cooldowntime);
        if (coolOrShoot == true)
        {
            var bullets = Instantiate(bullet, start.position, rotation.transform.rotation) as GameObject;
            var bullRig = bullets.GetComponent<Rigidbody>();
            bullRig.velocity = rotation.transform.forward * fireSpeed;
            //bullRig.AddForce(transform.forward * fireSpeed);
            Counter(1, multiFireRate, IsMultiRate);
        }
        else if(coolOrShoot == false)
        {
            canFire = true;
            bulletCounter = 0;
        }
    }
}
