using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
        public bool manager = Manager.gameStart;
	    public GameObject[] enemy;                // The enemy prefab to be spawned.
	    public float spawnTime;            // How long between each spawn.
	    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	    public static bool overpop = false;
        GameObject birds;
	
	    void Start ()
	    {
            //if(Manager.gameStart)
            //{
                // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
                InvokeRepeating("Spawn", Random.Range(0f, 2f), Random.Range(0f, 2f));
                Debug.Log("Spawn");
            //}  
	    }
	
	
	    void Spawn ()
	    {
		    if(overpop == false)
		    {
		    // Find a random index between zero and one less than the number of spawn points.
		    int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		    int enemyIndex = Random.Range (0, enemy.Length);
		    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		    birds = Instantiate (enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
                birds.transform.parent = transform.transform;
		    Population.counting();
		    }
	    }
}