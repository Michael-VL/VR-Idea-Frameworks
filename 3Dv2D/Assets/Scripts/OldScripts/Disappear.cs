using UnityEngine;
using System.Collections;

public class Disappear : MonoBehaviour {

    Renderer rend;
    public float alphaDecreaseRate;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    public void Change()
    {
        var mat = Instantiate(rend.material) as Material;
        rend.material = mat;
        var alphaColor = mat.color;
        if(alphaColor.a > 0)
        {
            alphaColor.a -= alphaDecreaseRate;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
