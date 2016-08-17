using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerVisionEffects : MonoBehaviour {

    NoiseAndGrain NoiseGrain;
    MotionBlur MotionBlur;
    BloomOptimized Bloom;

    public float timeUntilVisionClears;

    public float transitionSpeed;

    public float intensityGrain;
    public float blurAmount;
    public float bloomIntensity;

    void Start()
    {
        NoiseGrain = GetComponent<NoiseAndGrain>();
        MotionBlur = GetComponent<MotionBlur>();
        Bloom = GetComponent<BloomOptimized>();

        NoiseGrain.enabled = false;
        MotionBlur.enabled = false;
        Bloom.enabled = false;

        NoiseGrain.generalIntensity = 0;
        MotionBlur.blurAmount = 0;
        Bloom.intensity = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("collision vision");
        if(col.gameObject.tag == "Bullet")
        {
            //Debug.Log("distort vision");
            NoiseGrain.enabled = true;
            MotionBlur.enabled = true;
            Bloom.enabled = true;

            NoiseGrain.generalIntensity = Mathf.Lerp(0, intensityGrain, transitionSpeed * Time.deltaTime);
            MotionBlur.blurAmount = Mathf.Lerp(0, blurAmount, transitionSpeed * Time.deltaTime);
            Bloom.intensity = Mathf.Lerp(0, bloomIntensity, transitionSpeed * Time.deltaTime);

            StartCoroutine("Delay");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeUntilVisionClears);

        NoiseGrain.generalIntensity = Mathf.Lerp(intensityGrain, 0, transitionSpeed * Time.deltaTime);
        MotionBlur.blurAmount = Mathf.Lerp(blurAmount, 0, transitionSpeed * Time.deltaTime);
        Bloom.intensity = Mathf.Lerp(bloomIntensity, 0, transitionSpeed * Time.deltaTime);

        NoiseGrain.enabled = false;
        MotionBlur.enabled = false;
        Bloom.enabled = false;
    }
}
