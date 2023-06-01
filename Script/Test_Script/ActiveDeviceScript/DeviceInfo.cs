using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo: MonoBehaviour
{
    /// <summary>
    /// Device가 가지고 있는 기본적인 데이터
    /// 스크립트 Device 오브젝트에 연결
    /// </summary>
    ///
    [SerializeField] private float value; //pin으로부터 받는 value값

    [SerializeField] private int pinNumber; //C.C와 연결되 있는 Pin번호

    [SerializeField] private bool isOn; //pin으로부터 받는 bool값

    [SerializeField] private float[] inputKeyValue = { 0.0f, 0.0f, 0.0f, 0.0f }; //키마다 가지고 있을 값 에)w, a, s, d

    [SerializeField] private bool[] inputKeyDown = { false, false, false, false }; //키가 눌렸는지 확인하는 값 예)w, a, s, d

    //[SerializeField] private bool successCompile;

    public DeviceInfo()
    {
        value = 0;
        pinNumber = -1;
        isOn = false;
        //1111successCompile = false;
    }

    public DeviceInfo(float v, bool i)
    {
        value = v;
        isOn = i;
        //successCompile = false;
    }

    public DeviceInfo(float v, bool i, int p)
    {
        value = v;
        isOn = i;
        pinNumber = p;
        //successCompile = false;
    }

    public void setValue(float v)
    {
        this.value = v;
    }

    public float getValue()
    {
        return value;
    }

    public void setPinNumber(int number)
    {
        pinNumber = number;
    }

    public int getPinNumber()
    {
        return pinNumber;
    }

    public void setIsOn(bool isOn)
    {
        this.isOn = isOn;
    }

    public bool getIsOn()
    {
        return isOn;
    }

    public void setInputKeyValue(int index, float value)
    {
        if (index > (inputKeyValue.Length - 1))
            return;
        inputKeyValue[index] = value;
    }

    public float getInputKeyValue(int index)
    {
        if (index > (inputKeyValue.Length - 1))
            return 0.0f;
        return inputKeyValue[index];
    }

    public void setInputKeyDown(int index, bool keyDown)
    {
        if (index > (inputKeyDown.Length - 1))
            return;
        inputKeyDown[index] = keyDown;
    }

    public void setInputKeyDown(bool[] keyDown)
    {
        if (keyDown.Length != inputKeyDown.Length)
            return;
        inputKeyDown = keyDown;
    }

    public bool getInputKeyDown(int index)
    {
        if (index > (inputKeyDown.Length - 1))
            return false;
        return inputKeyDown[index];
    }

    public bool[] getInputKeyDown()
    {
        return inputKeyDown;
    }

    //public void setSuccessCompile(bool success)
    //{
    //    successCompile = success;
    //}

    //public bool getSuccessCompile()
    //{
    //    return successCompile;
    //}
}
