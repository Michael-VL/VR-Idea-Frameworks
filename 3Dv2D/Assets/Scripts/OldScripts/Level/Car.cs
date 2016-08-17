using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public float carSpeedMax;
    public float carSpeedMin;

    public GameObject
        carL;
    public GameObject carR;

    public Transform startR;
    public Transform startL;
    public Transform endR;
    public Transform endL;

    bool moveR;
    bool moveL;

    void Start()
    {
        carL.transform.position = startL.position;
        carR.transform.position = startR.position;
    }
    void Update()
    {
        //LeftCar
        if(carL.transform.position == startL.position)
        {
            moveL = true;
        }
        else if(carL.transform.position == endL.position)
        {
            moveL = false;
        }

        if(moveL)
        {
            carL.transform.LookAt(endL);
            carL.transform.position = Vector3.MoveTowards(carL.transform.position, endL.position, Random.Range(carSpeedMin, carSpeedMax));
        }
        else
        {
            carL.transform.LookAt(startL);
            carL.transform.position = Vector3.MoveTowards(carL.transform.position, startL.position, Random.Range(carSpeedMin, carSpeedMax));
        }

        //RightCar
        if (carR.transform.position == startR.position)
        {
            moveR = true;
        }
        else if (carR.transform.position == endR.position)
        {
            moveR = false;
        }

        if (moveR)
        {
            carR.transform.LookAt(endR);
            carR.transform.position = Vector3.MoveTowards(carR.transform.position, endR.position, Random.Range(carSpeedMin, carSpeedMax));
        }
        else
        {
            carR.transform.LookAt(startR);
            carR.transform.position = Vector3.MoveTowards(carR.transform.position, startR.position, Random.Range(carSpeedMin, carSpeedMax));
        }
    }
}
