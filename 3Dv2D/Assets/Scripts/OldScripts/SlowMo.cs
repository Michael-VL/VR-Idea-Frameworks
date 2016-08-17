using UnityEngine;
using System.Collections;

public class SlowMo : MonoBehaviour {
    public float speedUpRate;
    public float breakpoint;
    public float delay;

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

    public GameObject conL;
    public GameObject conR;
    public GameObject conH;

    public void Start()
    {
        Time.timeScale = .001f;
    }

    void Update()
    {
        delayed();
        //StartCoroutine("vDelay");

        velHC = conH.transform.position.magnitude;
        velLC = conL.transform.position.magnitude;
        velRC = conR.transform.position.magnitude;

        time = Time.timeScale;
        Mathf.Clamp(Time.timeScale, 0, 1);

        velH = ( velHC - velHD ) / Time.deltaTime;
        velL = ( velLC - velLD ) / Time.deltaTime;
        velR = ( velRC - velRD ) / Time.deltaTime;

        //Left
        if (velL > breakpoint)
        {
            Time.timeScale *= velL * speedUpRate;
        }
        //Right
        if (velR > breakpoint)
        {
            Time.timeScale *= velR * speedUpRate;
        }
        //Head
        if (velH > breakpoint)
        {
            Time.timeScale *= velH * speedUpRate;
        }
    }

    void delayed()
    {
        if (Time.realtimeSinceStartup >= (Time.realtimeSinceStartup + delay))
        {
            return;
        }

        velHD = conH.transform.position.magnitude;
        velLD = conL.transform.position.magnitude;
        velRD = conR.transform.position.magnitude;
    }

    //IEnumerator vDelay()
    //{
    //    yield return new WaitForSeconds(delay);

    //    velHD = conH.transform.position.magnitude;
    //    velLD = conL.transform.position.magnitude;
    //    velRD = conR.transform.position.magnitude;
    //}
}
