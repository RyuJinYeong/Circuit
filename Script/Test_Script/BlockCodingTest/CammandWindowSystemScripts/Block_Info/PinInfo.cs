using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinInfo : MonoBehaviour
{
    /// <summary>
	/// pin에 대한 정보(pin에 연결된 소자에 대한 정보)
    /// 추후 pin에 정보를 pin에 연결된 소자로 넘겨줌
    /// 스크립트 : pin블럭에 연결
	/// </summary>

    [SerializeField] private int pinNum; //pin number

    [SerializeField] private float value; //value블럭에서 전달하는 값

    [SerializeField] private bool isOn; //pin에 대한 on/off 값

    [SerializeField] private float[] inputKeyValue = { 0.0f, 0.0f, 0.0f, 0.0f }; //키마다 가지고 있을 값 에)w, a, s, d

    [SerializeField] private bool[] inputKeyDown = { false, false, false, false }; //키가 눌렸는지 확인하는 값 예)w, a, s, d

    [SerializeField] private GameObject actBlock; //해당 핀에 연결된 블럭

    [SerializeField] private BlockTypeEnum pinControlBlock; //해당 핀이 제어할 능동소자블럭(핀의 연결된 능동소자, 핀의 성질)

    public void setPinNum(int value)
    {
        pinNum = value;
    }

    public int getPinNum()
    {
        return pinNum;
    }

    public void setValue(float value)
    {
        this.value = value;
    }

    public float getValue()
    {
        return value;
    }

    public void setIsOn(bool isOn)
    {
        this.isOn = isOn;
    }

    public bool getIsOn()
    {
        return isOn;
    }

    public void setInputKeyValue(int index, float value)
    {
        if (index > (inputKeyValue.Length - 1))
            return;
        inputKeyValue[index] = value;
    }

    public float getInputKeyValue(int index)
    {
        if (index > (inputKeyValue.Length - 1))
            return 0.0f;
        return inputKeyValue[index];
    }

    public void setInputKeyDown(int index, bool keyDown)
    {
        if (index > (inputKeyDown.Length - 1))
            return;
        inputKeyDown[index] = keyDown;
    }

    public void setInputKeyDown(bool[] keyDown)
    {
        if (keyDown.Length != inputKeyDown.Length)
            return;
        inputKeyDown = keyDown;
    }

    public bool getInputKeyDown(int index)
    {
        if (index > (inputKeyDown.Length - 1))
            return false;
        return inputKeyDown[index];
    }

    public bool[] getInputKeyDown()
    {
        return inputKeyDown;
    }

    public void setActBlock(GameObject blk)
    {
        actBlock = blk;
    }

    public GameObject getActBlock()
    {
        return actBlock;
    }

    public void setPinControlBlock(BlockTypeEnum bte)
    {
        pinControlBlock = bte;
    }

    public BlockTypeEnum getPinControlBlock()
    {
        return pinControlBlock;
    }
}
