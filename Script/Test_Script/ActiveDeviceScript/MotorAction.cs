using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorAction : MonoBehaviour
{
    /// <summary>
    /// 모터가 가지고 있는 데이터
    /// 데이터를 바탕으로 모터가 동작하는 코드
    /// 스크립트 MotorDevice 오브젝트에 연결
    /// </summary>
    ///

    //각 디바이스별로 스크립트를 찾아서 데이터를 전달해주는 문제발생
    //정보를 담는 객체를 생성하는 방식으로 변경
    //[SerializeField] private float power; //모터가 동작하는 세기 (pin에서는 value값을 받음)

    //[SerializeField] private int pinNumber; //모터가 C.C와 연결된 pin번호

    //[SerializeField] private bool isOn; //모터에 전원이 공급되었는지 확인하는 bool변수

    [SerializeField] private DeviceInfo di; //Motor가 pin으로부터 받는 정보값

    [SerializeField] private GameObject connector; //모터가 움직일 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        di = gameObject.GetComponent<DeviceInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        MotorRun(di.getIsOn(), di.getValue());
    }

    //public void setPower(float power)
    //{
    //    this.power = power;
    //}

    //public float getPower()
    //{
    //    return power;
    //}

    //public void setPinNumber(int number)
    //{
    //    pinNumber = number;
    //}

    //public int getPinNumber()
    //{
    //    return pinNumber;
    //}    

    //public void setIsOn(bool isOn)
    //{
    //    this.isOn = isOn;
    //}

    //public bool getIsOn()
    //{
    //    return isOn;
    //}

    public void setConnector(GameObject obj)
    {
        connector = obj;
    }

    public GameObject getConnector()
    {
        return connector;
    }

    /// <summary>
    /// 모터의 동작에 대한 함수
    /// </summary>
    /// 
    void MotorRun(bool isOn, float power)
    {
        if (isOn)
        {
            connector.transform.Rotate(0, power * Time.deltaTime, 0);
        }
        return;
    }
}
