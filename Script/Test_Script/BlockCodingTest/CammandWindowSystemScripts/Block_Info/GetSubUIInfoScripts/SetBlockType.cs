using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetBlockType : MonoBehaviour
{
    /// <summary>
	/// 기존 디바이스 블럭을 개별로 만드는게 아닌 디바이스 블럭에서 드롭다운 메뉴를 추가해
    /// 하나의 디바이스 블럭에서 여러가지 종류의 디바이스를 설정하도록 하기 위한 스크립트
    /// 이 스크립트는 디바이스 블럭에 있는 드롭다운 메뉴를 선택하면 디바이스 블럭에 연결된 BlockType의 값을 변경시켜줌
    /// 스크립트 : 디바이스 블럭에 연결
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
        BlockType bt = this.GetComponent<BlockType>();

        switch(value)
        {
            case 0:
                //Debug.Log("Motor");
                bt.setBlockType(BlockTypeEnum.MotorBlock);
                break;
            case 1:
                //Debug.Log("Door");
                bt.setBlockType(BlockTypeEnum.DoorBlock);
                break;
            case 2:
                bt.setBlockType(BlockTypeEnum.InfraredSensorBlock);
                break;
        }
    }
}
