using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{

    public static bool space;
    public float speed;
    public float intensityMultiplierNight;
    public float rotation;
    public float timer;
    public float rotationDelay;
    Light sun;
    public float intensityMultiplier;
    public float timescale;
    public GameObject wolf;
    bool day;

    void Start()
    {
        sun = GetComponent<Light>();
    }
    void Update()
    {
        if(day)
        {
            wolf.SetActive(false);
        }
        else
        {
            wolf.SetActive(true);
        }
        Time.timeScale = timescale;
        rotation = transform.rotation.x;
        if (rotation < rotationDelay)
        {
            sun.intensity -= .25f * intensityMultiplierNight;
            day = false;
        }
        if (rotation > rotationDelay)
        {
            sun.intensity += .25f * intensityMultiplier;
            day = true;
        }

        if (space == false)
        {
            transform.Rotate(Vector3.right * Time.deltaTime, speed, Space.World);
        }
        StartCoroutine("timerDelay");
    }
    IEnumerator timerDelay()
    {
        yield return new WaitForSeconds(10f);
        rotationDelay = transform.rotation.x;
    }
}
