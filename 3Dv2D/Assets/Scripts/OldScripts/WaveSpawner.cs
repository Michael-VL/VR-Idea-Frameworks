using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    //array of numbers
    public GameObject[] availableEnemies;
    public int[] enemies;
    public List<Transform> availableSpawnPoints;
    public int[] spawnPoint;

    public GameObject arraySpawnPoints;

    public static bool waveEnd = false;
    public float timer;
    public float timeBetweenWave;
    public float savTimer;
    public int i = -1;

    public static int curPop;

    bool logged;
    float realTimeTrack;

    bool waveStart = true;

    public float testTime;

    public bool startOnEnter;
    //spawn enemy depending on number

    void Start()
    {

        savTimer = timer;
        foreach (Transform point in arraySpawnPoints.transform)
        {
            availableSpawnPoints.Add(point);
        }

        if (startOnEnter)
        {
            Manager.gameStart = true;
        }
    }

    void Update()
    {
        testTime = Time.realtimeSinceStartup;
        if (waveEnd)
        {
            timer = timeBetweenWave;
        }
        else
        {
            timer = savTimer;
        }
        if (curPop == 0 && !waveStart)
        {
            //Debug.Log("Pop0");
            waveEnd = true;
        }
        if(Manager.gameStart)
        {
            if (!logged)
            {
                //Debug.Log("logged");
                realTimeTrack = Time.realtimeSinceStartup;

                logged = true;
            }
            delayed();
        }

    }

    public static void counter(string option)
    {
        if (option == "add")
        {
            curPop += 1;
        }
        else if (option == "subtract")
        {
            curPop -= 1;
        }
    }

    void delayed()
    {
        if (Time.realtimeSinceStartup <= (realTimeTrack + timer))
        {
            return;
        }
        waveStart = false;
        logged = false;
        //Debug.Log("Out" + " " + (i+1));

        waveEnd = false;

        if (enemies[i] != 999 && enemies[i] != 888 && i < enemies.Length)
        {
            //Debug.Log("Spawning");
            Instantiate(availableEnemies[enemies[i]], availableSpawnPoints[spawnPoint[i]].transform.position, Quaternion.identity);
            counter("add");
            i += 1;
        }
        else if (enemies[i] == 888)
        {
            //Debug.Log("fullstop");
            transform.gameObject.SetActive(false);
            Manager.gameStart = false;
        }
        else if (enemies[i] == 999)
        {
            //Debug.Log("waveEnd");
            waveEnd = true;
            i += 1;
        }
        else
        {
            //Debug.Log("exception catch delay function");
            transform.gameObject.SetActive(false);
            Manager.gameStart = false;
        }
    }
}
