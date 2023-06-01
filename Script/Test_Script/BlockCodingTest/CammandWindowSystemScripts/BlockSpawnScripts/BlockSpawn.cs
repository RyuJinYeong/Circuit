using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSpawn : MonoBehaviour, IPointerClickHandler
{
    //스크립트 스폰블럭에 연결
    //start 블록같이 단일슬롯만 존재하는 블록에 대한 스폰
    public Canvas canvas; //현재 캔버스
    public GameObject designPanel; //놓여진 디자인 패널
    public GameObject objectToSpawn; //생성할 오브젝트
    public GameObject slot; //생성할 슬롯

    private GameObject spawnedObject; //생성한 오브젝트
    private GameObject spawnedSlot; //생성한 슬롯

    //private RectTransform transformCursor;

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

        //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
        spawnedObject.transform.SetParent(canvas.transform, false); //캔버스의 자식으로 지정
        spawnedSlot.transform.SetParent(canvas.transform, false);
        //spawnedObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1); //스케일 변경
        spawnedSlot.GetComponent<RectTransform>().localScale = new Vector2(2, 2);

        //복제한 슬롯의 부모오브젝트를 복제한 오브젝트로 설정
        spawnedSlot.GetComponent<Slot>().parentBlock = spawnedObject.GetComponent<RectTransform>();

        //복제한 오브젝트을 따라오는 슬롯에 대한 정보 설정
        spawnedObject.GetComponent<DragDrop>().slot = spawnedSlot;

        //복제한 오브젝트 위치를 마우스 위치로 변경
        //spawnedObject.GetComponent<RectTransform>().anchoredPosition = eventData.worldPosition;

        //복제한 오브젝트의 위치를 매 프레임마다 업데이트
        //StartCoroutine(FollowMouse());
    }

    /*
    IEnumerator FollowMouse()
    {
        while (Input.GetMouseButton(0))
        {
            spawnedObject.transform.position = Input.mousePosition;
            yield return null;
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
