using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Generator_Slot : MonoBehaviour//, IDropHandler
{
    /*
    //Generator 패널에 있는 오브젝트에 드래그할 경우
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; 아이템 슬롯으로 재배치
            //eventData.pointerDrag : 드래그 하고 있는 오브젝트
            eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
        }
    }
    */
}
