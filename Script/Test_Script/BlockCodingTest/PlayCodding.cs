using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCodding : MonoBehaviour
{
    //블록코딩을 할 window창을 띄우는 조건에 대한 스크립트
    //Command창은 동일, 커맨드 창을 채우는 블록들의 배열을 커맨드 디바이스에 따라 바꿈(스테이지에 따라)
    //Command창 띄울시 띄운 플레이어의 움직임 멈춤
    //스크립트 : 모니터 오브젝트에 연결

    [SerializeField] private Canvas canvas; //띄울 커맨드창

    public bool isCanvasOpen = false; //커맨드 창 활성화 여부

    public GameObject player; //커맨드 창을 활성화 시킨 플레이어

    // Update is called once per frame
    void Update()
    {
        if (isCanvasOpen)
        {
            //canvas.gameObject.SetActive(true);
            canvas.enabled = true;
            player.GetComponent<ChController>().enabled = false;
            player.GetComponent<PauseManager>().enabled = false;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = false;
                isCanvasOpen = false;
                //canvas.gameObject.SetActive(false);
                canvas.enabled = false;
                player.GetComponent<ChController>().enabled = true;
                player.GetComponent<PauseManager>().enabled = true;
            }
        }
    }
}
