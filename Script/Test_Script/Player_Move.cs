using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.Translate(Vector3.right * 3.0f * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            this.transform.Translate(Vector3.forward * 3.0f * Input.GetAxis("Vertical") * Time.deltaTime);
        }
    }
}