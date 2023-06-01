using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    //스크립트 드래그가 필요한 코드블럭에 연결

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject designPanel; //design panel

    private RectTransform rectTransform;
    //private RectTransform canvasRectTransform; //캔버스의 RectTransform;
    private RectTransform designPanelRectTransform; //design panel의 RectTransform
    private CanvasGroup canvasGroup;

    private Vector3 rectPosition = Vector3.zero;

    private bool isOutSide; //UI 요소가 캔버스 밖으로 나갔는지 확인
    public bool isDrop = false; //슬롯에 놓여졌는지 확인
    //public bool isActDrop = false; //활성화 블럭에 놓여졌는지 확인 (ActivitySlot.cs)
    public bool isStartBlock = false; //시작 블럭인지 확인
    public bool inCondition = false; //조건문 안에 있는 블럭인지 확인
    public bool isRight = false; //연산자 블럭의 오른쪽 블럭인지 확인

    public bool isStatic = false; //캔버스가 시작할 때 같이 있는 블럭 isStatic = true : 삭제X
    //public bool isFixed = false; //블럭의 드래그를 막는 변수 isFixed = true : drag X, slot의 위치를 조정하여 구현(변수 필요X)

    //public GameObject prefabSlot; //prefabSlot 조건부 슬롯 생성시 필요 
    public GameObject slot = null; //블럭을 따라오는 슬롯
    public GameObject actSlot = null; //활성화 블럭 슬롯(블럭 오른쪽에 위치한 슬롯)
    public RectTransform InputSlotRectTransform; //블록이 들어간 슬롯의 RectTransform

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //canvasRectTransform = canvas.GetComponent<RectTransform>();
        designPanelRectTransform = designPanel.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        //캔버스(패널)와 오브젝트의 경계 영역 구하기
        Vector3[] canvasCorners = new Vector3[4];
        //canvasRectTransform.GetWorldCorners(canvasCorners); //캔버스와 오브젝트의 영역을 구할 때
        designPanelRectTransform.GetWorldCorners(canvasCorners); //디자인 패널과 오브젝트의 영역을 구할 때

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        //Rect rect = new Rect(corners[0].x, corners[1].y, rectTransform.rect.width, rectTransform.rect.height);

        Rect canvasRect = new Rect(canvasCorners[0], canvasCorners[2] - canvasCorners[0]);
        Rect rect = new Rect(corners[0], corners[2] - corners[0]);

        Vector2 minPosition = new Vector2(rect.xMin - canvasRect.xMin, rect.yMin - canvasRect.yMin);
        Vector2 maxPosition = new Vector2(rect.xMax - canvasRect.xMax, rect.yMax - canvasRect.yMax);

        // UI 요소가 캔버스 밖으로 나갔는지 확인
        isOutSide = (minPosition.x < 0f) || (maxPosition.x > 0f) || (minPosition.y < 0f) || (maxPosition.y > 0f);
        //Debug.Log(isOutSide);
        //Debug.Log(isDrop);

        //슬롯에 놓여져 있을 때
        if(isDrop)
        {
            try //선두 블럭과 연결이 끊어졌을 때 처리하는 코드(쓰레기통에 들어갔을 때 처리하는 코드)
            {
                this.GetComponent<RectTransform>().anchoredPosition = InputSlotRectTransform.anchoredPosition;
            }
            catch (MissingReferenceException e) //여러 블럭이 연결되어 있을 경우 제일 선두의 블럭을 초기 세팅으로 바꿔줌
            {
                //Debug.Log(e.Message);
                InputSlotRectTransform = null;
                isDrop = false;
                isRight = false;
                inCondition = false;

                if (actSlot != null && !this.CompareTag("ConditionBlock"))
                {
                    actSlot.GetComponent<Slot>().setInCondition(this.gameObject, false); //actSlot에 연결된 dropObject의 inCondition = false로 변경

                    //Value블럭이고 dropObject가 없을 때
                    if ((this.GetComponent<BlockType>().bte == BlockTypeEnum.ValueBlock) && actSlot.GetComponent<Slot>().dropObject == null)
                    {
                        actSlot.SetActive(false); //가상블럭 비활성화
                    }
                }

                ////가상블럭 비활성화
                //switch (this.gameObject.GetComponent<BlockType>().bte)
                //{
                //    case BlockTypeEnum.ValueBlock:
                //    //case BlockTypeEnum.MotorBlock:
                //    //case BlockTypeEnum.DoorBlock:
                //        actSlot.SetActive(false);
                //        break;
                //}

                if (slot != null)
                {
                    slot.SetActive(true);
                }

                this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1000);
            }
        }
    }

    //드래그 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        isDrop = false;
        isRight = false;

        inCondition = false;

        if (actSlot != null && !this.CompareTag("ConditionBlock"))
        {
            actSlot.GetComponent<Slot>().setInCondition(this.gameObject, false); //actSlot에 연결된 dropObject의 inCondition = false로 변경

            //Value블럭이고 dropObject가 없을 때
            if((this.GetComponent<BlockType>().bte == BlockTypeEnum.ValueBlock) && actSlot.GetComponent<Slot>().dropObject == null)
            {
                actSlot.SetActive(false); //가상블럭 비활성화
            }
        }

        ////가상블럭 비활성화
        //switch (this.gameObject.GetComponent<BlockType>().bte)
        //{
        //    case BlockTypeEnum.ValueBlock:
        //    //case BlockTypeEnum.MotorBlock:
        //    //case BlockTypeEnum.DoorBlock:
        //        actSlot.SetActive(false);
        //        break;
        //}

        if (slot != null)
        {
            slot.SetActive(true);
        }

        rectPosition = rectTransform.position; //드래그를 시작할 때 위치
        //Debug.Log(rectPosition);
        canvasGroup.alpha = 0.6f; //불투명도
        canvasGroup.blocksRaycasts = false;
    }

    //드래그 하고 있을 때
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //canvas의 scaleFactor 만큼 보정치를 줌(마우스 따라가는 지연문제) screen space - camera 일때
    }

    //드래그 끝났을 때
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        
        if(isOutSide)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
        }

        canvasGroup.alpha = 1f; //불투명도
        canvasGroup.blocksRaycasts = true;
    }
    //마우스 버튼으로 눌렀을 때
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPonterDown");

        //이미지의 우선순위 설정
        this.GetComponent<RectTransform>().SetAsLastSibling(); //하이라키에서 위치를 마지막으로 변경(동일한 상위 오브젝트 상에서)
    }

    /// <summary>
    /// Block오브젝트 위에 무언가가 올라가 있을 경우
    /// 추후 수정할 내용:
    /// 현재 슬롯에 있던 블럭을 다른 블럭에 옮길경우 제자리로 돌아와 얼핏보면 블럭과 연결되있는 것처럼 보이지만
    /// 실제로는 연결이 끊긴것처럼 보임
    /// </summary>
    /// 
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DragDrop_OnDrop");
        //슬롯에 놓여져 있을 때
        if(isDrop)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().getPosition(); // 원래 있던 위치로 배치
        }
        //throw new System.NotImplementedException();
    }

    public void setCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    public void setDesignPanel(GameObject designPanel)
    {
        this.designPanel = designPanel;
    }

    public Vector3 getPosition()
    {
        return rectPosition;
    }
}
