using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour {
	
	public GameObject prefab;
	public int numberOfObjects = 10;
	public float radius;
	public GameObject[] cubes;
	GameObject allPrefabs;
	public float intensity;
	AudioSource audio;
	public float height;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3 (radius*Mathf.Cos (angle), 0, radius*Mathf.Sin (angle));
			allPrefabs = Instantiate (prefab, pos, Quaternion.identity) as GameObject;
			allPrefabs.transform.parent = this.transform;
			allPrefabs.transform.position += Vector3.up * height * Time.deltaTime;
			audio = GetComponent<AudioSource>(); 
		}
		cubes = GameObject.FindGameObjectsWithTag ("cubes");
	}
	
	// Update is called once per frame
	void Update () {
		float[] spectrum = audio.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		for (int i = 0; i < numberOfObjects; i++) {
			Vector3 previousScale = cubes[i].transform.localScale;
			previousScale.y = Mathf.Lerp (previousScale.y, spectrum [i] * intensity, Time.deltaTime * 30);
			cubes[i].transform.localScale = previousScale;
		}
	}
}
