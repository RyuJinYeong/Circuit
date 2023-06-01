using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSpawn_Total : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Block을 스폰하는 코드를 하나로 통합
    /// Block스폰을 세세하게 컨트롤 가능해짐
    /// 종류
    /// 1) 단일 블럭 : slot이 없는 블럭(value, device ...)
    /// 2) 변수 블럭 : 일반 slot과 우측에 actSlot을 가진 블럭 (pin)
    /// 3) 조건 블럭 : 일반 slot과 우측에 conditionSlot을 가진 블럭(If)
    /// 스크립트 : 블럭 스포너에 연결
    /// </summary>
    /// 
    public Canvas canvas; //현재 캔버스
    public GameObject designPanel; //놓여진 디자인 패널
    public GameObject objectToSpawn; //생성할 오브젝트
    public GameObject slot; //생성할 슬롯

    private GameObject spawnedObject; //생성한 오브젝트
    private GameObject spawnedSlot; //생성한 슬롯
    private GameObject spawnedSideSlot; //생성한 조건슬롯

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        BlockSpawner(objectToSpawn);
    }

    public void BlockSpawner(GameObject objectToSpawn)
    {
        BlockTypeEnum bte = objectToSpawn.GetComponent<BlockType>().bte; //생성하고자 하는 블럭의 타입을 받아옴

        //공통 생성 부분
        //복제할 오브젝트의 캔버스(패널)를 설정
        objectToSpawn.GetComponent<DragDrop>().setCanvas(canvas);
        objectToSpawn.GetComponent<DragDrop>().setDesignPanel(designPanel);

        //클릭한 오브젝트 복제
        spawnedObject = Instantiate(objectToSpawn);

        //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
        //spawnedObject.transform.SetParent(canvas.transform, false); //캔버스의 자식으로 지정
        spawnedObject.transform.SetParent(designPanel.transform, false);

        switch (bte)
        {
            //어떤 slot도 가지고 있지 않는 블럭
            case BlockTypeEnum.MotorBlock:
            case BlockTypeEnum.SwitchBlock:
            case BlockTypeEnum.DoorBlock:
            case BlockTypeEnum.InputKeyBlock:
                break;
            //가상의 actSlot만 가지고 있는 블럭(ex:조건문에서만 활성화)
            case BlockTypeEnum.ValueBlock:
                //복제할 오브젝트의 캔버스(패널)를 설정
                slot.GetComponent<Slot>().canvas = canvas;

                //클릭한 오브젝트 복제
                spawnedSideSlot = Instantiate(slot);

                //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
                //spawnedSideSlot.transform.SetParent(canvas.transform, false);
                spawnedSideSlot.transform.SetParent(designPanel.transform, false);

                //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
                spawnedSideSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

                //슬롯의 우선순위 설정
                spawnedSideSlot.GetComponent<RectTransform>().SetAsFirstSibling(); //하이라키에서 위치를 첫번째로 변경

                //복제한 SideSlot의 위치설정
                spawnedSideSlot.GetComponent<Slot>().setGabPos(new Vector2(110.0f, 0.0f));

                //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
                spawnedObject.GetComponent<DragDrop>().actSlot = spawnedSideSlot;

                //SideSlot의 태그 설정
                spawnedSideSlot.tag = "ValueSlot";
                spawnedSideSlot.SetActive(false);
                break;
            //actSlot만 생성하는 블럭
            case BlockTypeEnum.OperatorBlock:
                //복제할 오브젝트의 캔버스(패널)를 설정
                slot.GetComponent<Slot>().canvas = canvas;

                //클릭한 오브젝트 복제
                spawnedSideSlot = Instantiate(slot);

                //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
                //spawnedSideSlot.transform.SetParent(canvas.transform, false);
                spawnedSideSlot.transform.SetParent(designPanel.transform, false);

                //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
                spawnedSideSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

                //슬롯의 우선순위 설정
                spawnedSideSlot.GetComponent<RectTransform>().SetAsFirstSibling(); //하이라키에서 위치를 첫번째로 변경

                //복제한 SideSlot의 위치설정
                spawnedSideSlot.GetComponent<Slot>().setGabPos(new Vector2(110.0f, 0.0f));

                //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
                spawnedObject.GetComponent<DragDrop>().actSlot = spawnedSideSlot;

                //SideSlot의 태그 설정
                spawnedSideSlot.tag = "OperatorSlot";
                break;
            //actSlot과 slot을 생성하는 블럭
            case BlockTypeEnum.PinBlock:
                //복제할 오브젝트의 캔버스(패널)를 설정
                slot.GetComponent<Slot>().canvas = canvas;

                //클릭한 오브젝트 복제
                spawnedSlot = Instantiate(slot);
                spawnedSideSlot = Instantiate(slot);

                //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
                //spawnedSlot.transform.SetParent(canvas.transform, false);
                //spawnedSideSlot.transform.SetParent(canvas.transform, false);
                spawnedSlot.transform.SetParent(designPanel.transform, false);
                spawnedSideSlot.transform.SetParent(designPanel.transform, false);

                //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
                spawnedSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();
                spawnedSideSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

                //슬롯의 우선순위 설정
                spawnedSlot.GetComponent<RectTransform>().SetAsFirstSibling();
                spawnedSideSlot.GetComponent<RectTransform>().SetAsFirstSibling(); //하이라키에서 위치를 첫번째로 변경

                //복제한 SideSlot의 위치설정
                spawnedSideSlot.GetComponent<Slot>().setGabPos(new Vector2(110.0f, 0.0f));

                //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
                spawnedObject.GetComponent<DragDrop>().slot = spawnedSlot;
                spawnedObject.GetComponent<DragDrop>().actSlot = spawnedSideSlot;

                //SideSlot의 태그 설정
                spawnedSideSlot.tag = "ActSlot";
                break;
            //conditionSlot을 생성하는 블럭
            case BlockTypeEnum.IfBlock:
                //복제할 오브젝트의 캔버스(패널)를 설정
                slot.GetComponent<Slot>().canvas = canvas;

                //클릭한 오브젝트 복제
                spawnedSlot = Instantiate(slot);
                spawnedSideSlot = Instantiate(slot);

                //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
                //spawnedSlot.transform.SetParent(canvas.transform, false);
                //spawnedSideSlot.transform.SetParent(canvas.transform, false);
                spawnedSlot.transform.SetParent(designPanel.transform, false);
                spawnedSideSlot.transform.SetParent(designPanel.transform, false);

                //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
                spawnedSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();
                spawnedSideSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

                //슬롯의 우선순위 설정
                spawnedSlot.GetComponent<RectTransform>().SetAsFirstSibling();
                spawnedSideSlot.GetComponent<RectTransform>().SetAsFirstSibling(); //하이라키에서 위치를 첫번째로 변경

                //복제한 Slot의 위치설정
                spawnedSlot.GetComponent<Slot>().setGabPos(new Vector2(55.0f, -110.0f));

                //복제한 SideSlot의 위치설정
                spawnedSideSlot.GetComponent<Slot>().setGabPos(new Vector2(110.0f, 0.0f));

                //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
                spawnedObject.GetComponent<DragDrop>().slot = spawnedSlot;
                spawnedObject.GetComponent<DragDrop>().actSlot = spawnedSideSlot;

                //SideSlot의 태그 설정
                spawnedSideSlot.tag = "ConditionSlot";
                break;
        }
    }
}
