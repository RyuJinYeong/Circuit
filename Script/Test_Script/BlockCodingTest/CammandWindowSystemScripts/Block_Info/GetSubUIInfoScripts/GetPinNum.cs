using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPinNum : MonoBehaviour
{
    /// <summary>
    /// pin number를 pin의 PinInfo.cs로 전달해주는 스크립트
    /// pinBlock 내부의 드롭다운 메뉴와 상호작용
    /// </summary>

    [SerializeField] private TMP_Dropdown dropdown; //핀에 연결된 드롭다운 UI

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetChild(0).gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        //Debug.Log("Dropdown value : "+ value);
        this.GetComponent<PinInfo>().setPinNum(value);
    }
}
