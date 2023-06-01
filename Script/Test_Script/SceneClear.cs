using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClear : MonoBehaviour
{
    [SerializeField] GameObject[] ClearSwitch = new GameObject[5];
    bool clear = false;

    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player") && clear)
        {
            c.GetComponent<Clear>().isClear = true;
        }
    }

    private void Update()
    {
        for(int i = 0; i<5; i++)
        {
            if (ClearSwitch[i].GetComponent<SwitchDeviceController>().isUp)
            {
                clear = false;
                break;
            }
            else
            {
                clear = true;
            }
        }        
    }
}
