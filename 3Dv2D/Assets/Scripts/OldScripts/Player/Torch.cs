using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {

    public GameObject wolf;
    public GameObject fire;
    public Light torchLight;
    public GameObject flames;
    public Light campLight;
    public float Waking;

    bool alight;
    
    void Awake()
    {
        campLight.intensity = 0;
        torchLight.intensity = 0;
        flames.SetActive(false);
    }
    void Start()
    {
        alight = true;
        wolf.SetActive(false);
        fire.SetActive(false);
    }
    void Update()
    {
        if(alight)
        {
            StartCoroutine("wake");
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Fire" && fire.activeInHierarchy == false && wolf.activeInHierarchy == false)
        {
            fire.SetActive(true);
            wolf.SetActive(true);
            campLight.intensity = Mathf.Clamp(torchLight.intensity, 0, 2f);
            campLight.intensity += .05f;
        }
    }
    IEnumerator wake()
    {
        yield return new WaitForSeconds(Waking);
        torchLight.intensity = Mathf.Clamp(torchLight.intensity, 0, 4.55f);
        torchLight.intensity += .05f;
        
        flames.SetActive(true);
        alight = false;
    }
}
