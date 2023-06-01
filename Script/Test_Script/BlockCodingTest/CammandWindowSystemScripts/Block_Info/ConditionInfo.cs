using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionInfo : MonoBehaviour
{
    /// <summary>
	/// ConditionBlock이 가지고 있는 정보
    /// 스크립트 ConditionBlock에 연결
	/// </summary>
    ///

    public GameObject ifBlock; //else문과 연결된 if블럭
    public GameObject exitBlock; //if문이 끝나는 지점의 블럭
    public GameObject elseBlock; //이 if문과 연결된 else블럭

    [SerializeField] private bool result; //조건문 실행결과를 저장할 변수

    public void setResult(bool result)
    {
        this.result = result;
    }

    public bool getResult()
    {
        return result;
    }
}
