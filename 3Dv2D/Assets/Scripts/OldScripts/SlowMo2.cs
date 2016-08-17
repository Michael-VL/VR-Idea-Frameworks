using UnityEngine;
using System.Collections;

public class SlowMo2 : MonoBehaviour {
    [Header("Default normal passage of time")]
    [Space(10)]
    public float normTime;

    [Header("Altered passage of time")]
    [Space(10)]
    public float altTime;

    [Header("If slower, small fixed delta time; else, large fixed delta time")]
    [Space(10)]
    public float adjustedFixedDeltaTime;

    [Header("How fast to go to normal time")]
    public float fastTimeTransitionRate;

    float slowDownTime;
    [Header("Delay until it becomes normal")]
    [Space(10)]
    public float slowDownTimeLimit;

    SteamVR_TrackedObject trackedObjL;
    SteamVR_TrackedObject trackedObjR;
    SteamVR_TrackedObject trackedObjH;

    public float breakpoint;
    [Header("Delay for tracking")]
    [Space(10)]
    public float delay;

    [Header("Controllers")]
    [Space(10)]
    public GameObject conL;
    public GameObject conR;
    public GameObject conH;

    [Header("Monitoring Values")]
    [Space(10)]
    public float time;

    public float velL;
    public float velR;
    public float velH;

    public float velLD;
    public float velRD;
    public float velHD;

    public float velLC;
    public float velRC;
    public float velHC;

    bool logged = false;
    float savedRealTime;

    void Start()
    {
        trackedObjL = conL.GetComponent<SteamVR_TrackedObject>();
        trackedObjR = conR.GetComponent<SteamVR_TrackedObject>();
        trackedObjH = conH.GetComponent<SteamVR_TrackedObject>();

        Time.timeScale = .1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    void Update()
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.1f, 1);

        delayed();

        velHC = conH.transform.position.magnitude;
        velLC = conL.transform.position.magnitude;
        velRC = conR.transform.position.magnitude;

        time = Time.timeScale;
        Mathf.Clamp(Time.timeScale, 0, 1);

        velH = (velHC - velHD) / Time.deltaTime;
        velL = (velLC - velLD) / Time.deltaTime;
        velR = (velRC - velRD) / Time.deltaTime;

        var deviceL = SteamVR_Controller.Input((int)trackedObjL.index);
        var deviceR = SteamVR_Controller.Input((int)trackedObjR.index);

        //Left
        if (velL > breakpoint)
        {
            //Time.timeScale = fastTime;
            Time.timeScale = Mathf.Lerp(normTime, altTime, fastTimeTransitionRate * Time.deltaTime);
        }
        //Right
        else if (velR > breakpoint)
        {
            //Time.timeScale = fastTime;
            Time.timeScale = Mathf.Lerp(normTime, altTime, fastTimeTransitionRate * Time.deltaTime);
        }
        //Head
        else if (velH > breakpoint)
        {
            //Time.timeScale = fastTime;
            Time.timeScale = Mathf.Lerp(normTime, altTime, fastTimeTransitionRate * Time.deltaTime);
        }
        //Shoot
        else if (deviceL.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) || deviceR.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Time.timeScale = Mathf.Lerp(normTime, altTime, fastTimeTransitionRate * Time.deltaTime);
            
            Time.fixedDeltaTime = adjustedFixedDeltaTime;
        }

        //Delay until slow down
        if(Time.timeScale == normTime)
        {
            slowDownTime += Time.unscaledDeltaTime;
        }

        //Slowing back down
        if(slowDownTime > slowDownTimeLimit)
        {
            Time.fixedDeltaTime = Time.timeScale * adjustedFixedDeltaTime;
            slowDownTime = 0;
            Time.timeScale = altTime;
            //Time.timeScale = Mathf.Lerp(fastTime, .1f, fastTimeTransitionRate);
        }
    }

    void delayed()
    {
        if(logged == false)
        {
            savedRealTime = Time.realtimeSinceStartup;
            logged = true;
        }
        if(Time.realtimeSinceStartup < (savedRealTime + delay))
        {
            return;  
        }
        
            logged = false;

            velHD = conH.transform.position.magnitude;
            velLD = conL.transform.position.magnitude;
            velRD = conR.transform.position.magnitude;
    }
}
