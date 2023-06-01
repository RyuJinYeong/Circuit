using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInputKey : MonoBehaviour
{
    /// <summary>
	/// 값을 수정할 키보드의 버튼을 선택하는 스크립트
    /// 스크립트 : InputKey 블럭에 연결
	/// </summary>
    ///

    [SerializeField] private TMP_Dropdown dropdown; //디바이스 블럭에 연결된 드롭다운 UI

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetChild(0).gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        GetComponent<InputKeyInfo>().setKeyIndex(value);
    }
}
