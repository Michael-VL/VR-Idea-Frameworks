using UnityEngine;
using System.Collections;

public class ShooterAIAttack : MonoBehaviour {

    public GameObject bullet;
    public Transform target;
    public Transform gunEnd;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ShootAnim()
    {
        //Debug.Log("shooting animation triggered");
        
        gunEnd.LookAt(target);
        anim.SetTrigger("Fire");
    }

    public void BulletShooterAI()
    {
        gunEnd.LookAt(target);
        Instantiate(bullet, gunEnd.position, gunEnd.rotation);
    }
}
