using UnityEngine;
using System.Collections;

public class WolfAI : MonoBehaviour{

	public float speed;
    float orgSpeed;
	public Transform target;
    public float distance;
    public float maxdistance;
    public float torchDistance;
    public float maxTorchDistance;
    public Transform torch;
    public bool? running;
    bool look;
    public float stopping;

    public float startTime;
    public float time;
    public float timeUntil;
    public float orgTimeUntil;

    public Light torchLight;
    public ParticleSystem part;
    public ParticleSystem flamePart;

    Animator anim;
    public Light health;
    public AudioSource sounds;
    public AudioClip[] growlClips;
    int growlIndex;
    public AudioClip[] attackingClips;
    int attackingIndex;
    public AudioClip[] hurtClips;
    int hurtIndex;
    public float velo;

    public GameObject button;

    Rigidbody wolfRig;
    public bool death;

    public Light campFire;
    public float fireHealth;
    //public float fireHealthRate;
    public float improveRate;
    //static public bool active = false;
    public float torchOut;

    bool soundRun = true;
    bool soundCircle = true;
    bool soundHurt = true;

    public float soundDelay;

    public GameObject campFlames;
    public GameObject torchFlames;

    public float attackingDist;

    void Start()
    {
        orgSpeed = speed;
        //button.SetActive(false);
        startTime = Time.time;
        orgTimeUntil = timeUntil;
        anim = GetComponent<Animator>();
        wolfRig = GetComponent<Rigidbody>();
    }
	void Update()
	{
        if (death == false)
        {
            torchDistance = Vector3.Distance(transform.position, target.position);
            distance = Vector3.Distance(transform.position, target.position);
            time = Time.time - startTime;

            timeUntil -= time;

            if (timeUntil <= 0)
            {
                Run();
                running = true;
                startTime = Time.time;
                timeUntil = orgTimeUntil;
            }

            if (running == true)
            {
                Run();
                anim.SetBool("Run", true);
                anim.SetBool("Circle", false);
            }

            if (running == false)
            {
                Circle();
                anim.SetBool("Run", false);
                anim.SetBool("Circle", true);
            }

            speed = Mathf.Clamp(speed, 5, 5.2f);
            speed = Mathf.Lerp(speed, 5, Time.time/improveRate);
        }
        if (death == true)
        {
            torchLight.intensity = Mathf.Clamp(torchLight.intensity, 0, 4.55f);
            torchLight.intensity -= torchOut;

            campFire.intensity  = Mathf.Clamp(campFire.intensity, 0, 2f);
            campFire.intensity -= (torchOut + .05f);
            if (torchLight.intensity == 0)
            {
                torchFlames.SetActive(false);
                campFlames.SetActive(false);
                StartCoroutine("Flames");
            }
        }
        
    }
    void Run()
    {
        if(soundRun)
        {
            StartCoroutine("runSound");
            //StopCoroutine("runSound");
        }
            if (look)
            {
                transform.LookAt(target.transform);
                look = false;
            }
               
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //if (torchDistance < maxTorchDistance)
            //{
            //    running = false;
            //}
            if(distance < stopping)
            {
                //anim.SetTrigger("Jump");
                //running = false;
                //torchLight.intensity = Mathf.MoveTowards(torchLight.intensity, torchLight.intensity - 1, 1);
                //health.spotAngle += 20;
                //part.emissionRate -= -10;
                //flamePart.emissionRate -= 20;
                //if(health.spotAngle >= 60)
                //{
                //    Time.timeScale = 0;
                //}
            }
    }
    
    void Circle()
    {
        if (soundCircle)
        {
            StartCoroutine("runCircle");
            //StopCoroutine("runCircle");
        }
        look = true;
            if (distance < maxdistance)
            {
            //transform.Rotate(Vector3.up, 180);
            //Application.LoadLevel("Snow Scene");
                transform.position += -transform.forward * speed * Time.deltaTime;
                maxdistance = Random.Range(10, 50);
            }
            else
            {
                transform.LookAt(target.position, Vector3.up);
                transform.Rotate(Vector3.up, 90);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
    }
    
    void OnTriggerEnter(Collider col)
    {
        
        Debug.Log("Detected");
        if(col.gameObject.tag == "PickUp")
        {
            Rigidbody rigwolf = col.GetComponent<Rigidbody>();
            if(rigwolf.velocity.magnitude > velo)
            {
                if (soundHurt)
                {
                    StartCoroutine("hurtSound");
                    //StopCoroutine("hurtSound");
                }
                running = false;
                Debug.Log("Hurt");
                hurtIndex = Random.Range(0, hurtClips.Length);
                sounds.PlayOneShot(hurtClips[hurtIndex]);
                //transform.position += -transform.forward * speed * Time.deltaTime;
            }
           
        }
        if (col.gameObject.tag == "Player")
        {
            hurt.play = true;

            anim.SetTrigger("Jump");
            running = false;
            //torchLight.intensity = Mathf.MoveTowards(torchLight.intensity, torchLight.intensity - 1, 1);
            //health.spotAngle += 20;
            part.emissionRate -= -10;
            flamePart.emissionRate -= 20;

            campFire.intensity = Mathf.Lerp(campFire.intensity, campFire.intensity - fireHealth, Time.time);
            if (campFire.intensity <= .5)
            {
                //button.SetActive(true);
                wolfRig.isKinematic = true;
                death = true;
                anim.enabled = false;
            }
        }
       
    }
    IEnumerator runCircle()
    {
        Debug.Log("Hear");
        soundCircle = false;
        yield return new WaitForSeconds(soundDelay);
        attackingIndex = Random.Range(0, growlClips.Length);
        sounds.PlayOneShot(growlClips[growlIndex]);
        soundCircle = true;
    }
    IEnumerator runSound()
    {
        Debug.Log("Hear1");
        soundRun = false;
        yield return new WaitForSeconds(soundDelay);
        attackingIndex = Random.Range(0, attackingClips.Length);
        sounds.PlayOneShot(attackingClips[attackingIndex]);
        soundRun = true;
    }
    IEnumerator hurtSound()
    {
        Debug.Log("Hear2");
        soundHurt = false;
        yield return new WaitForSeconds(soundDelay);
        attackingIndex = Random.Range(0, hurtClips.Length);
        sounds.PlayOneShot(hurtClips[hurtIndex]);
        soundHurt = true;
    }
    IEnumerator Flames()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(1);
    }
}
