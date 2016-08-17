using UnityEngine;
using System.Collections;

public class GeneralShooting : MonoBehaviour {

    public Transform barrelEnd;
    //public Transform speed;
    public GameObject bullet;
    //public int ammo;


    //shoot
    public void Shoot()
    {
            Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);

    }

}
