using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour {

	public int maxpop;
	public static int counter = 0;

	void Update () 
	{
		if (counter >= maxpop) 
		{
			EnemySpawn.overpop = true;
		}
		if (counter < maxpop) 
		{
			EnemySpawn.overpop = false;
		}
	}

	public static void counting ()
	{
		counter += 1;
	}
	public static void death()
	{
		counter -= 1;
	}
}
