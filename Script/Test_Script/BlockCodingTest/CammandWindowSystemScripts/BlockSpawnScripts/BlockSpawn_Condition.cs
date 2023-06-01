using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSpawn_Condition : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 조건문을 가지는 블럭을 생성하는 스크립트(if문 또는 for문 while문 등)
    /// 스크립트 : 관련 블럭 스포너에 연결
    /// </summary>
    /// 
    public Canvas canvas; //현재 캔버스
    public GameObject designPanel; //놓여진 디자인 패널
    public GameObject objectToSpawn; //생성할 오브젝트
    public GameObject slot; //생성할 슬롯

    private GameObject spawnedObject; //생성한 오브젝트
    private GameObject spawnedSlot; //생성한 슬롯
    private GameObject spawnedConditionSlot; //생성한 조건슬롯

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        //복제할 오브젝트의 캔버스(패널)를 설정
        objectToSpawn.GetComponent<DragDrop>().setCanvas(canvas);
        objectToSpawn.GetComponent<DragDrop>().setDesignPanel(designPanel);
        slot.GetComponent<Slot>().canvas = canvas;

        //클릭한 오브젝트 복제
        spawnedObject = Instantiate(objectToSpawn);
        spawnedSlot = Instantiate(slot);
        spawnedConditionSlot = Instantiate(slot);

        //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
        spawnedObject.transform.SetParent(canvas.transform, false); //캔버스의 자식으로 지정
        spawnedSlot.transform.SetParent(canvas.transform, false);
        spawnedConditionSlot.transform.SetParent(canvas.transform, false);

        //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
        spawnedSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();
        spawnedConditionSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

        //복제한 ActSlot의 위치설정
        spawnedConditionSlot.GetComponent<Slot>().setGabPos(new Vector2(160.0f, 0.0f));

        //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
        spawnedObject.GetComponent<DragDrop>().slot = spawnedSlot;
        spawnedObject.GetComponent<DragDrop>().actSlot = spawnedConditionSlot;

        //ActSlot의 태그 설정
        spawnedConditionSlot.tag = "ConditionSlot";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
