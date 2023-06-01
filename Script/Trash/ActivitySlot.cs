using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
public class ActivitySlot : MonoBehaviour, IDropHandler
{
    public RectTransform parentBlock; //슬롯을 부여할 블럭오브젝트
    public Canvas canvas; //슬롯이 위치한 캔버스

    private Vector2 anchoredPos;
    private Vector2 gapPos = new Vector2(160.0f, 0.0f); //부모로 부터 떨어진 값

    public GameObject dropObject = null; //슬롯에 들어가 있는 블럭오브젝트

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        //Debug.Log("IsMount : "+ eventData.pointerDrag.gameObject.name);
        //드롭된 오브젝트가 UI요소인지 확인
        if (eventData.pointerDrag != null)
        {
            if (dropObject != null) //슬롯에 블럭이 놓여져 있을 때
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
            }
            else //슬롯에 블럭이 놓여져 있지 않을 때
            {
                if (eventData.pointerDrag.gameObject.GetComponent<DragDrop>().isStartBlock) //시작 블럭이면
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
                    return;
                }
                dropObject = eventData.pointerDrag.gameObject; //슬롯에 들어간 블럭오브젝트
                dropObject.GetComponent<DragDrop>().isActDrop = true;
                dropObject.GetComponent<DragDrop>().InputSlotRectTransform = this.GetComponent<RectTransform>();
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = anchoredPos; //아이템 슬롯으로 재배치
            }
        }

        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anchoredPos = parentBlock.GetComponent<RectTransform>().anchoredPosition + gapPos; //부모의 anchored위치값 + 부모로부터 떨어진 값

        //슬롯에 놓여진 블럭이 빠질 때 
        if (dropObject != null)
        {
            if (!dropObject.GetComponent<DragDrop>().isActDrop)
                dropObject = null;
        }

        this.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
    }
}
*/