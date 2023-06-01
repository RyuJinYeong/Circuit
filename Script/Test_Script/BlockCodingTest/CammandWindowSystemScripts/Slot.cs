using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    //스크립트 슬롯블럭에 연결

    public RectTransform parentBlock; //슬롯을 부여할 블럭오브젝트
    public Canvas canvas; //슬롯이 위치한 캔버스

    private Vector2 anchoredPos;
    [SerializeField] private Vector2 gapPos = new Vector2(0.0f, -110.0f); //부모로 부터 떨어진 값

    private Color defaultColor;

    public GameObject dropObject = null; //슬롯에 들어가 있는 블럭오브젝트

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        //Debug.Log("IsMount : "+ eventData.pointerDrag.gameObject.name);

        //드롭된 오브젝트가 UI요소인지 확인
        if (eventData.pointerDrag != null)
        {
            GameObject dragObject = eventData.pointerDrag;
            if (dropObject != null) //슬롯에 블럭이 놓여져 있을 때
            {
                dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
            }
            else //슬롯에 블럭이 놓여져 있지 않을 때
            {
                if(dragObject.GetComponent<DragDrop>().isStartBlock) //시작 블럭이면
                {
                    dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                    return;
                }
                if (gameObject.CompareTag("ActSlot")) //이 슬롯이 actSlot이면
                {
                    //Debug.Log("this slot is actSlot");
                    if (!dragObject.CompareTag("ActBlock")) //ActBlock : 특정한 값을 가지는 블럭 또는 동작을 소자에 대한 블럭
                    {
                        //ActBlock이 아니면 원래 있던 위치로 배치
                        dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }

                    //미구현 기능으로 인한 임시적 조치
                    if(dragObject.GetComponent<BlockType>().bte == BlockTypeEnum.InputKeyBlock) //InputKeyBlock이 ActSlot에 들어오면
                    {
                        if (!this.parentBlock.GetComponent<DragDrop>().inCondition)
                        {
                            dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                            return;
                        }
                    }

                    //이 slot의 부모 블럭의 isRight가 true면
                    if (parentBlock.GetComponent<DragDrop>().isRight)
                    {
                        //isRight = true에서 ActBlock으로 올수 있는 블럭은 ValueBlock만 가능하도록 함
                        //컴파일에서 제어하는 방식으로 변경예정
                        //if(dragObject.GetComponent<BlockType>().bte != BlockTypeEnum.ValueBlock)
                        //{
                        //    dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        //    return;
                        //}
                        setIsRight(dragObject);
                    }

                    //이 slot의 부모 블럭의 inCondition이 true이면
                    if (parentBlock.GetComponent<DragDrop>().inCondition)
                    {
                        //dragObject.GetComponent<DragDrop>().inCondition = true; //dropObject의 inCondition = true
                        //if(dragObject.GetComponent<DragDrop>().actSlot != null) //가상 블럭이 있다면
                        //{
                        //    dragObject.GetComponent<DragDrop>().actSlot.SetActive(true); //가상 블럭 활성화
                        //}
                        setInCondition(dragObject, true);
                    }
                }
                else if(gameObject.CompareTag("ConditionSlot")) //이 슬롯이 conditionSlot이면
                {
                    //Debug.Log("OnDrop ConditionSlot");
                    if (!dragObject.CompareTag("DataBlock")) //ConditionBlock의 조건문 첫번째 블럭의 테그가 DataBlock이 아니면 (pinBlock)
                    {
                        dragObject.GetComponent<RectTransform>().position = dragObject.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }
                    dropObject = dragObject; //슬롯에 들어간 블럭오브젝트
                    dropObject.GetComponent<DragDrop>().isDrop = true;
                    //dropObject.GetComponent<DragDrop>().inCondition = true;
                    setInCondition(dropObject, true);
                    dropObject.GetComponent<DragDrop>().InputSlotRectTransform = this.GetComponent<RectTransform>();
                    dragObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos; //아이템 슬롯으로 재배치

                    //dropObject의 slot에 할당된 블럭 연결해제
                    if(dropObject.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject != null) //dropObject가 null이 아니면
                    {
                        dropObject.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject.GetComponent<DragDrop>().InputSlotRectTransform = null;
                        dropObject.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject.GetComponent<DragDrop>().isDrop = false;
                        dropObject.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject.GetComponent<DragDrop>().inCondition = false;
                        dropObject.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject = null;
                    }
                    dropObject.GetComponent<DragDrop>().slot.SetActive(false); //DataBlock의 일반 슬롯 비활성화
                    return;
                }
                else if(gameObject.CompareTag("OperatorSlot")) //이 슬롯이 OperationSlot이면
                {
                    //OperationBlock이 가지고 있는 슬롯, DataBlock이나 ActBlock만 들어갈 수 있음
                    if (!(dragObject.CompareTag("ActBlock") || dragObject.CompareTag("DataBlock")))
                    {
                        //ActBlock이 아니면 원래 있던 위치로 배치
                        //DataBlock이 아니면 원래 있던 위치로 배치
                        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }

                    //ActBlock은 Value블럭만 오도록 하는 코드 -> 컴파일에서 제어하는 방식으로 변경예정
                    //if(dragObject.CompareTag("ActBlock"))
                    //{
                    //    if (parentBlock.CompareTag("ActBlock") || dragObject.GetComponent<BlockType>().bte != BlockTypeEnum.ValueBlock)
                    //    {
                    //        //연결하려는 블럭이 ActBlock이고 이전에 연결된 블럭이 ActBlock이면 원래 있던 위치로 배치ㅋ
                    //        //연결하려는 블럭이 ActBlock이고 연결하려는 블럭의 타입이 ValueBlock이 아니면
                    //        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                    //        return;
                    //    }
                    //}

                    if (dragObject.CompareTag("ActBlock") && parentBlock.CompareTag("ActBlock"))
                    {
                        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }

                    setIsRight(dragObject);

                    //이 slot의 부모 블럭의 inCondition이 true이면
                    if (parentBlock.GetComponent<DragDrop>().inCondition)
                    {
                        setInCondition(dragObject, true);
                    }
                }
                else if(gameObject.CompareTag("ValueSlot")) //Value블럭이 가지고 있는 슬롯 Operator블럭만 들어갈 수 있음
                {
                    if(!dragObject.CompareTag("OperatorBlock"))
                    {
                        //OperatorBlock이 아니면 원래 위치로 재배치
                        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }

                    //이 slot의 부모 블럭의 isRight가 true면
                    if (parentBlock.GetComponent<DragDrop>().isRight)
                    {
                        setIsRight(dragObject);
                    }

                    //이 slot의 부모 블럭의 inCondition이 true이면
                    if (parentBlock.GetComponent<DragDrop>().inCondition)
                    {
                        setInCondition(dragObject, true);
                    }
                }
                //else if(gameObject.CompareTag("InputSlot")) //InputKey블럭이 가지고 있는 actSlot -> value블럭만 들어갈 수 있음(컴파일에서 제어)
                //{
                //    if (!dragObject.CompareTag("ActBlock"))
                //    {
                //        //ActBlock이 아니면 원래 있던 위치로 배치
                //        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                //        return;
                //    }
                //}
                else //일반 슬롯이면
                {
                    if (!(dragObject.CompareTag("DataBlock") || dragObject.CompareTag("ConditionBlock") || dragObject.CompareTag("PrimeBlock")))
                    {
                        //DataBlock이거나 ConditionBlock이 아니면
                        //원래 위치로 재배치
                        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }

                    //미구현 기능으로 인한 임시적 조치
                    if (dragObject.GetComponent<BlockType>().bte == BlockTypeEnum.CalculateBlock) //CalculateBlock이면
                    {
                        dragObject.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                        return;
                    }
                }
                dropObject = dragObject; //슬롯에 들어간 블럭오브젝트
                dropObject.GetComponent<DragDrop>().isDrop = true;
                dropObject.GetComponent<DragDrop>().InputSlotRectTransform = this.GetComponent<RectTransform>();
                dragObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos; //아이템 슬롯으로 재배치
            }
        }

        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //anchoredPos = parentBlock.GetComponent<RectTransform>().anchoredPosition + gapPos; //부모의 anchored위치값 + 부모로부터 떨어진 값
        anchoredPos = parentBlock.anchoredPosition + gapPos;

        //if(!parentBlock.GetComponent<DragDrop>().isFixed)
        //{
        //    anchoredPos = parentBlock.anchoredPosition + gapPos;
        //}

        //슬롯에 놓여진 블럭이 있을 때
        if(dropObject != null)
        {
            GetComponent<Image>().color = Color.green;
            if (!dropObject.GetComponent<DragDrop>().isDrop) //isDrop이 false가 되면 즉 드래그를 시작할 때
            {
                dropObject = null;
            }
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
        
        this.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
    }

    public void setGabPos(Vector2 gapPos)
    {
        this.gapPos = gapPos;
    }

    /// <summary>
    /// ConditionSlot에 블럭이 들어왔을 때 블럭에 연결된 모든 블럭의 InCondition의 값을 바꿔주는 함수
    /// 매개변수 firstBlock : 처음 시작할 블럭
    /// 매개변수 isIn : InCondition을 설정할 bool 값
    /// </summary>
    /// 
    public void setInCondition(GameObject firstBlock, bool isIn)
    {
        GameObject block = firstBlock;
        //dropObject가 없을 때까지 추후 endBlock까지로 수정예정
        while(block != null)
        {
            block.GetComponent<DragDrop>().inCondition = isIn;

            //isIn = true && 현재 블럭에 slot이 있으면 비활성화
            if(isIn && block.GetComponent<DragDrop>().slot != null)
            {
                block.GetComponent<DragDrop>().slot.SetActive(!isIn);
            }
            //isIn = true && 현재 블럭에 actBlock이 있으면 활성화
            if(isIn && block.GetComponent<DragDrop>().actSlot != null && !block.GetComponent<DragDrop>().isRight)
            {
                block.GetComponent<DragDrop>().actSlot.SetActive(isIn);
            }

            if (block.GetComponent<DragDrop>().actSlot != null)
            {
                block = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //다음에 연결된 블럭
            }
            else
            {
                block = null;
            }
        }
    }

    /// <summary>
    /// OperationSlot에 블럭이 들어왔을 때 블럭에 연결된 모든 블럭의 isRight값을 바꿔주는 함수
    /// 매개변수 firstBlock : 처음 시작할 블럭
    /// </summary>
    /// <param name="firstBlock"></param>
    public void setIsRight(GameObject firstBlock)
    {
        GameObject block = firstBlock;
        //dropObject가 없을 때까지 추후 endBlock까지로 수정예정
        while (block != null)
        {
            block.GetComponent<DragDrop>().isRight = true;

            if (block.GetComponent<DragDrop>().slot != null)
            {
                block.GetComponent<DragDrop>().slot.SetActive(false);
            }
            if (block.GetComponent<DragDrop>().actSlot != null)
            {
                block = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //다음에 연결된 블럭
            }
            else
            {
                block = null;
            }
        }
    }
}
