using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSpawn_Single : MonoBehaviour, IPointerClickHandler
{
    //스크립트 스폰블럭에 연결
    //end와 같이 슬롯이 없는 블럭의 스폰
    public Canvas canvas; //현재 캔버스
    public GameObject designPanel; //놓여진 디자인 패널
    public GameObject objectToSpawn; //생성할 오브젝트

    private GameObject spawnedObject; //생성한 오브젝트

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        //복제할 오브젝트의 캔버스(패널)를 설정
        objectToSpawn.GetComponent<DragDrop>().setCanvas(canvas);
        objectToSpawn.GetComponent<DragDrop>().setDesignPanel(designPanel);

        //클릭한 오브젝트 복제
        spawnedObject = Instantiate(objectToSpawn);

        //복제한 오브젝트의 위치를 블럭생성하는 블럭으로 변경
        spawnedObject.transform.SetParent(canvas.transform, false); //캔버스의 자식으로 지정
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
