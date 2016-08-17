using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class U : MonoBehaviour {

    public List<Transform> vrTransforms;
    public List<Transform> pcTransforms;

    public float offsetY;

    void Update()
    {
        for(var i = 0; i < vrTransforms.Count; i++)
        {
            var offsetvrTransforms = new Vector3(vrTransforms[i].position.x, vrTransforms[i].position.y + offsetY, vrTransforms[i].position.z);
            pcTransforms[i].position = offsetvrTransforms;

            if(i == vrTransforms.Count)
            {
                i = 0;
            } 
        }
    }
}
