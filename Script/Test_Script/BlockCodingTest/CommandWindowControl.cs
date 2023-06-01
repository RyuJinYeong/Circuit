using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWindowControl : MonoBehaviour
{
    //블록코딩을 할 window창을 띄우는 조건에 대한 스크립트
    //Command창은 동일, 커맨드 창을 채우는 블록들의 배열을 커맨드 디바이스에 따라 바꿈(스테이지에 따라)
    //추후 최적화 방법 : 플레이어가 F버튼을 눌렀을 때 레이케스트를 이용한 거리계산

    //Canvas의 지정방식 : 플레이어마다 Canvas를 따로 가지는 경우와 하나의 공통된 Canvas를 가지는 경우 -> 개발 방향에 따라 수정할 것
    public Canvas canvas; //띄울 커맨드창

    private bool isCanvasOpen = false; //커맨드 창 활성화 여부

    [SerializeField] private GameObject player; //커맨드 창을 활성화 시킨 플레이어

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //거리측정후 특정 거리가 되면 커맨드 창 활성화 되도록
        //커맨드 디바이스의 정면 방향으로 Ray 발사
        Ray ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1.0f, Color.red); //scen 창에서 Ray를 시각적으로 그려줌
        RaycastHit hit; //Ray와 부딪친 오브젝트

        //커맨드 창 활성화 조건
        if(Physics.Raycast(ray, out hit, 1.0f)) //Raycast의 길이 1.0f로 제한
        {
            //Debug.Log("hit distanace : " + hit.distance);
            if ((hit.transform.CompareTag("Player1") || (hit.transform.CompareTag("Player2")))) //부딪힌 물체가 플레이어이고 거리가 1.0f 미만이면
            {
                Debug.Log("Hit!!");
                player = hit.transform.gameObject; //부딪친 플레이어 지정
                if (Input.GetKeyDown(KeyCode.F)) //키보드 F를 누르면 커맨드 창 활성화
                {
                    canvas.gameObject.SetActive(true);
                    isCanvasOpen = true;
                }
            }
        }

        //커맨드 창 비활성화 조건
        if(isCanvasOpen)
        {
            //커맨드 창 활성화 되면 플레이어의 움직임 멈춤
            player.GetComponent<Player_Move>().enabled = false;

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                canvas.gameObject.SetActive(false);
                player.GetComponent<Player_Move>().enabled = true;
                isCanvasOpen = false;
            }
        }
    }

    public bool getIsCanvasOpen()
    {
        return isCanvasOpen;
    }
}
