using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public Rigidbody attachPoint;
    FixedJoint joint;

    SteamVR_TrackedObject trackedObj;
    public LineRenderer line;
    public bool trigger;
    public float influenceDistance;
    public float distance;

    public bool bowling;
    GameObject go;

    bool collide;
    bool holding;

    SpringJoint spring;

    void Awake()
    {
        attachPoint = GetComponent<Rigidbody>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        line = GetComponent<LineRenderer>();
        spring = GetComponent<SpringJoint>();
    }
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;

    }

    void OnTriggerEnter(Collider col)
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (col.gameObject.tag == "PickUp")
        {
            collide = true;
        }
        
        if (collide && holding == false && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            holding = true;
            go = col.gameObject;
            //go.transform.parent = transform;
            var rigidbody = go.GetComponent<Rigidbody>();

            //rigidbody.isKinematic = true;
            //rigidbody.useGravity = false;

            spring.connectedBody = rigidbody;
        }
    }
    void OnTriggerExit(Collider col)
    {
        collide = false;
    }
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (holding == false && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //line.enabled = true;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            //line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                //line.SetPosition(1, hit.point);
                distance = Vector3.Distance(transform.position, hit.point);
            }
            else
            {
                //line.SetPosition(1, ray.GetPoint(100));
            }
            if (hit.rigidbody && distance <= influenceDistance && hit.transform.tag == "PickUp")
            {
                //var directionVector = -(hit.transform.position - transform.position).normalized;
                //transform.rotation = Quaternion.LookRotation(directionVector);
                go = hit.transform.gameObject;
                //go.transform.position = attachPoint.transform.position;

                //joint = go.AddComponent<FixedJoint>();
                //joint.connectedBody = attachPoint;
                
                //go.transform.parent = transform;
                var rigidbody = go.GetComponent<Rigidbody>();
                //rigidbody.isKinematic = true;
                //rigidbody.useGravity = false;

                spring.connectedBody = rigidbody;

                holding = true;
            }
        }
        if (holding == true && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            trigger = false;
            //line.enabled = false;
            
            //var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            //go.transform.position = go.transform.position;
            //Object.DestroyImmediate(joint);
            //joint = null;

            //rigidbody.isKinematic = false;
            //rigidbody.useGravity = true;

            //go.transform.parent = null;

            spring.connectedBody = null;

            holding = false;

        //    var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        //    if (origin != null)
        //    {
        //        rigidbody.velocity = origin.TransformVector(device.velocity);
        //        rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
        //    }
        //    else
        //    {
        //        rigidbody.velocity = device.velocity;
        //        rigidbody.angularVelocity = device.angularVelocity;
        //    }

        //    rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }
        //else
        //{
        //    line.enabled = false;
        //}
    }
}