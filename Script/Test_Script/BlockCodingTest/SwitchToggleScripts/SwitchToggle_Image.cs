using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle_Image : MonoBehaviour
{
    public Sprite backGroundActiveImage; //바뀔 이미지

    public Sprite backGroundDefaultImage; //기본 이미지

    public Image backGroundImage;

    Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        backGroundImage = this.transform.GetChild(0).gameObject.transform.GetComponent<Image>();

        backGroundDefaultImage = backGroundImage.sprite;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    private void OnSwitch(bool on)
    {
        backGroundImage.sprite = on ? backGroundActiveImage : backGroundDefaultImage;
    }
}
