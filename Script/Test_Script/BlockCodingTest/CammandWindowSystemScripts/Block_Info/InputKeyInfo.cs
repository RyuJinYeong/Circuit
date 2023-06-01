using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyInfo : MonoBehaviour
{
    /// <summary>
    /// InputKey블럭이 키보드의 어떤 버튼을 나타내는지 정보를 가지고 있음(주소값)
    /// 스크립트 : InputKeyBlock에 연결
    /// </summary>
    ///

    [SerializeField] private int keyIndex = 0; //키가 저장된 인덱스

    public void setKeyIndex(int idx)
    {
        keyIndex = idx;
    }

    public int getKeyIndex()
    {
        return keyIndex;
    }
}
