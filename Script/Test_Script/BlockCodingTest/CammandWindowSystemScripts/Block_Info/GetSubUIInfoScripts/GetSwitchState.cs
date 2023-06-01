using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSwitchState : MonoBehaviour
{
    /// <summary>
    /// switchBlock 내부의 toggle의 반환값을 SwitchInfo에 전달해줌
    /// 스크립트 switchBlock에 연결
    /// </summary>
    ///

    [SerializeField] private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = transform.GetChild(0).gameObject.GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnSwitch);
    }

    private void OnSwitch(bool on)
    {
        this.GetComponent<SwitchInfo>().setIsOn(on);
    }
}
