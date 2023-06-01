using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetConditionType : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown; //디바이스 블럭에 연결된 드롭다운 UI

    public GameObject slot;
    public GameObject actSlot;

    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<DragDrop>().slot;
        actSlot = GetComponent<DragDrop>().actSlot;
        dropdown = transform.GetChild(0).gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0: //If
                this.GetComponent<BlockType>().bte = BlockTypeEnum.IfBlock;
                slot.GetComponent<Slot>().setGabPos(new Vector2(55.0f, -110.0f));
                actSlot.SetActive(true);
                break;
            case 1: //Else
                this.GetComponent<BlockType>().bte = BlockTypeEnum.ElseBlock;
                slot.GetComponent<Slot>().setGabPos(new Vector2(55.0f, -110.0f));
                actSlot.SetActive(false);
                break;
            case 2: //Exit
                this.GetComponent<BlockType>().bte = BlockTypeEnum.ExitBlock;
                slot.GetComponent<Slot>().setGabPos(new Vector2(-55.0f, -110f));
                actSlot.SetActive(false);
                break;
            case 3: //Calculate
                this.GetComponent<BlockType>().bte = BlockTypeEnum.CalculateBlock;
                slot.GetComponent<Slot>().setGabPos(new Vector2(0.0f, -110.0f));
                actSlot.SetActive(true);
                break;
        }
    }
}
