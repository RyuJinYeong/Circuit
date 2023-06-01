using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveElementFind : MonoBehaviour
{
    //CommandController와 연결된 능동소자(모터, 센서 등)를 찾고 스크립트로 제어하기 위해 정리하는 코드
    //스크립트 : 모니터에 연결

    public GameObject[] activeElement; //연결된 능동소자

    // Start is called before the first frame update
    void Start()
    {
        activeElement = GetComponent<MonitorController>().connectGameObject;
        //activeElement[0].GetComponent<DeviceInfo>().setPinNumber(0);
        //activeElement[1].GetComponent<DeviceInfo>().setPinNumber(1);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < activeElement.Length; i++)
        {
            if (activeElement[i] != null)
            {
                //Debug.Log("activeLength:"+activeElement.Length);
                activeElement[i].GetComponent<DeviceInfo>().setPinNumber(i);
                //activeElement[i].GetComponent<DeviceInfo>().setSuccessCompile(GetComponent<MonitorController>().successCompile);
            }
        }
    }
}
