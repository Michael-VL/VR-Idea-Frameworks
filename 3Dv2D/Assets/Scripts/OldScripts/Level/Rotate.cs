using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float XSpeed;
    public float YSpeed;
    public float ZSpeed;

	void Update () {
		transform.Rotate (XSpeed * Time.deltaTime, YSpeed * Time.deltaTime, ZSpeed * Time.deltaTime);
	}
}
