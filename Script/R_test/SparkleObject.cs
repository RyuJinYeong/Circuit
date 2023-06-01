using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleObject : MonoBehaviour
{
    public Material[] mats;
    public float frameInterval = 3f;

    private float currentFrame = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentFrame++;

        if(frameInterval < currentFrame)
        {
            GetComponent<MeshRenderer>().material = mats[1];
            currentFrame = 0;
        }
        else
        {
            GetComponent<MeshRenderer>().material = mats[0];
        }
    }
}
