using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject _door;
    private int needfulBat = 1;

    void Update()
    {
        if (GetComponent<BatteryDeviceController>().equipBatCount >= needfulBat)
        {
            _door.GetComponent<Collider>().enabled = true;
        }
        else
        {
            _door.GetComponent<Collider>().enabled = false;
        }
    }
}
