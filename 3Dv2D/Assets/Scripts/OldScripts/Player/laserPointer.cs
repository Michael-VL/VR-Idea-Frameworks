using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]

public class laserPointer : MonoBehaviour {
	SteamVR_TrackedObject trackedObj;
	public float maxDistance;
	public float distance;
	public bool trigger;
	public LineRenderer line;

    public float power;

    public GameObject start;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		line = GetComponent<LineRenderer> ();
	}

	void Update () 
	{
		line.SetPosition(0, start.transform.position);

		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			trigger = true;
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			trigger = false;
		}

		if (trigger) 
		{
			line.enabled = true;
			
			Ray ray = new Ray (start.transform.position, start.transform.forward);
			RaycastHit hit;
			
			line.SetPosition (0, ray.origin);
			
			if (Physics.Raycast (ray, out hit, maxDistance)) {
				line.SetPosition (1, hit.point);
				if (hit.rigidbody) {
					hit.rigidbody.AddForceAtPosition (transform.forward * power, hit.point);
				}
			} else {
				line.SetPosition (1, ray.GetPoint (maxDistance));
			}
		} 
		else 
		{
			line.enabled = false;
		}
	}
}
