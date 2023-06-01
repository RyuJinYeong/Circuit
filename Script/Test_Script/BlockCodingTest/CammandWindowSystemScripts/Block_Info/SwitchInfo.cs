using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInfo : MonoBehaviour
{
    /// <summary>
    /// switchBlock이 가지고 있는 정보
    /// 스크립트 switchBlock에 연결
    /// </summary>
    ///

    [SerializeField] private bool isOn;

    public void setIsOn(bool isOn)
    {
        this.isOn = isOn;
    }

    public bool getIsOn()
    {
        return isOn;
    }
}
