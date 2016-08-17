using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

    public static bool gameStart = false;
    public static bool gameEnd = false;

    public static List<GameObject> guns;

    public static float nearestDist = 100;
    public static GameObject nearestGun;

    public GameObject Player;
    public static float distFromPlayer;
    public static GameObject _Player;

    public GameObject wave;

    void Start()
    {
        _Player = Player;

    }

    void Update()
    {
        if (gameStart)
        {
            wave.SetActive(true);
        }
        
    }
    //public static void AddGun(GameObject gun)
    //{
    //    guns.Add(gun);

    //}

    //public static void NearestGun(GameObject trans)
    //{
    //    foreach (GameObject gun in guns)
    //    {
    //        var distance = Vector3.Distance(trans.transform.position, gun.transform.position);

    //        if (distance < nearestDist)
    //        {
    //            nearestDist = distance;
    //            nearestGun = gun;
    //            distFromPlayer = Vector3.Distance(_Player.transform.position, nearestGun.transform.position);
    //        }
    //    }
    //    if(nearestDist > distFromPlayer)
    //    {
    //        nearestGun = null;
    //        return;
    //    }
    //}

}
