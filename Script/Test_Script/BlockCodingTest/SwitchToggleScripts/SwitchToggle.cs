using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    //code reference
    //https://github.com/herbou/Unity_SwitchToggleUI

    [SerializeField] RectTransform uiHandleRectTransform; //switch toggle의 핸들오브젝트
    [SerializeField] Color backgroundActiveColor; //toggle on일 경우 바뀔 색깔

    Image backgroundImage; //toggle background 이미지 객체

    Color backgroundDefaultColor; //toggle 배경기본색상

    Toggle toggle;

    Vector2 handlePosition; //핸들 오브젝트의 위치

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    private void OnSwitch (bool on)
    {
        /*
        if (on)
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
        else
            uiHandleRectTransform.anchoredPosition = handlePosition; */

        //토글을 클릭했을 경우 -> isOn = true
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
