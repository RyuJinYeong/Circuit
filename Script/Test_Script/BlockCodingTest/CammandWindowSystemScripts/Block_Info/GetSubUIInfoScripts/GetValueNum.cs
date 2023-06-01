using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetValueNum : MonoBehaviour
{
    /// <summary>
    /// value block의 value값을 전달하는 스크립트
    /// value block 내부의 toggle과 textinput과 상호작용
    /// 스크립트 value block에 연결
    /// </summary>

    [SerializeField] private TMP_InputField tmpInput; //block에 연결된 textInputField
    [SerializeField] private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        tmpInput = transform.GetChild(0).gameObject.GetComponent<TMP_InputField>();
        toggle = transform.GetChild(1).gameObject.GetComponent<Toggle>();
        
        tmpInput.onValueChanged.AddListener(OnInputFieldChanged);
        toggle.onValueChanged.AddListener(OnSwitch);
    }

    private void OnInputFieldChanged(string value)
    {
        //Debug.Log("InputField value : " + value);
        this.GetComponent<ValueInfo>().setValue(int.Parse(value));
    }

    private void OnSwitch(bool on)
    {
        this.GetComponent<ValueInfo>().setIsPositive(!on);
    }
}