using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueInfo : MonoBehaviour
{
    /// <summary>
    /// value block이 가지고 있는 정보에 대한 스크립트
    /// 스크립트 value block에 연결
    /// </summary>
    // Start is called before the first frame update

    [SerializeField] private int value; //value block의 value값
    [SerializeField] private bool isPositive; //양수 음수 확인하는 bool값

    public void setValue(int value)
    {
        this.value = value;
    }

    public int getValue()
    {
        return value;
    }

    public void setIsPositive(bool isPos)
    {
        isPositive = isPos;
    }

    public bool getIsPositive()
    {
        return isPositive;
    }
}
