using UnityEngine;
using System.Collections;

public class MoveUntilPoint : MonoBehaviour {

    public float speed;
    public bool enablemovement;
    public GameObject colider;

    // Update is called once per frame
    void Update()
   {
        transform.position = Vector3.MoveTowards(transform.position, colider.transform.position, speed);
   }

    // transform.position = Vector3.Lerp(transform.position, colider.transform.position, speed* Time.deltaTime);

}
