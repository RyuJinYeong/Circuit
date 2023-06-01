using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnTrash");
        if (!(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().CompareTag("PrimeBlock") || eventData.pointerDrag.gameObject.GetComponent<DragDrop>().isStatic)) //필수 블럭이 아니면 실행
        {
            //if(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().actSlot != null)
            //{
            //    Destroy(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().actSlot);
            //}
            Destroy(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().actSlot);
            Destroy(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().slot); //슬롯 삭제
            Destroy(eventData.pointerDrag.gameObject); //블럭 삭제
            return;
        }
        eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
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
