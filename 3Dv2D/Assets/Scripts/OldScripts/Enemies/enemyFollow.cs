using UnityEngine;
using System.Collections;

public class enemyFollow : MonoBehaviour {

	public Transform target;
	public float speed;
	public float health;
	public float maxdistance;
	public float distance;
	public float avoid;

	void Update()
	{
		if (health <= 0) {
			Destroy(gameObject);
		}
		//transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
		transform.position += -transform.right * speed * Time.deltaTime;
		transform.LookAt (target.position, Vector3.up);
		transform.Rotate (Vector3.up, 90);
//		RaycastHit hit;
//		if(Physics.Raycast(hit, Vector3.forward, maxdistance))
//		{
//			distance = hit.distance;
//			if(distance < 10)
//			{
//				avoid += avoid;
//				transform.Rotate(Vector3.up, avoid);
//			}
//			else
//			{
//				avoid = 1;
//				transform.LookAt (target.position, Vector3.up);
//			}
//		}
	}
	void damage(float damage)
	{
		health -= damage;
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player") {
			Destroy(target.gameObject);
		}
	}
}
