using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorInfo : MonoBehaviour
{
    /// <summary>
    /// operatorBlock이 가지고 있는 정보
    /// </summary>
    ///

    [SerializeField] private char oper = '+'; //operator 값

    public void setOperator(char oper)
    {
        this.oper = oper;
    }

    public char getOperator()
    {
        return oper;
    }
}
