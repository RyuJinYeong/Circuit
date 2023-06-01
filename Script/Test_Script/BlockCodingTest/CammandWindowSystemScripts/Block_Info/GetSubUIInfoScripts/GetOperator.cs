using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetOperator : MonoBehaviour
{
    /// <summary>
    /// dropdown메뉴에서 설정한 오퍼레이터값을 operatorBlock으로 넘겨주는 스크립트
    /// 스크립트 operatorBlock에 연결
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
        switch (value)
        {
            case 0: //+
                this.GetComponent<OperatorInfo>().setOperator('+');
                break;
            case 1: //-
                this.GetComponent<OperatorInfo>().setOperator('-');
                break;
            case 2: //*
                this.GetComponent<OperatorInfo>().setOperator('*');
                break;
            case 3: //'/'
                this.GetComponent<OperatorInfo>().setOperator('/');
                break;
            case 4: //=
                this.GetComponent<OperatorInfo>().setOperator('=');
                this.GetComponent<BlockType>().bte = BlockTypeEnum.ComparisonBlock; //블럭타입 : 비교연산자블럭으로 변경
                break;
            case 5: //>
                this.GetComponent<OperatorInfo>().setOperator('>');
                this.GetComponent<BlockType>().bte = BlockTypeEnum.ComparisonBlock;
                break;
            case 6: //<
                this.GetComponent<OperatorInfo>().setOperator('<');
                this.GetComponent<BlockType>().bte = BlockTypeEnum.ComparisonBlock;
                break;
        }
    }
}
