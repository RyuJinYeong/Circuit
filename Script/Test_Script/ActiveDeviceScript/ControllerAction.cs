using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAction : MonoBehaviour
{
    /// <summary>
    /// Controller의 동작에 관한 스크립트
    /// Controller에 접속한 후 키보드 버튼의 클릭에 따라 컨트롤러가 제어한 오브젝트의 동작이 달라짐
    /// 입력의 현재상태를 전송 및 DeviceInfo데이터를 가지고 있음
    /// 스크립트 : 컨트롤러에 연결
    /// </summary>
    ///

    [SerializeField] DeviceInfo di;

    [SerializeField] private bool isAccess; //player접속여부

    public GameObject player; //접속한 플레이어

    [SerializeField] GameObject controlGameObject; //컨트롤러로 컨트롤할 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        di = GetComponent<DeviceInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(di.getIsOn() && isAccess) //controller의 전원이 켜지면
        {
            ControllerRun();
            //player.GetComponent<ChController>().enabled = false;
            player.GetComponent<ChController>().speed = 0;
            player.GetComponent<ChController>().jumpSpeed = 0;
            player.GetComponent<PauseManager>().enabled = false;
            controlGameObject.GetComponent<DummyAction>().setDeviceInfo(di);
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isAccess = false;
                //player.GetComponent<ChController>().enabled = true;
                player.GetComponent<ChController>().speed = 6;
                player.GetComponent<ChController>().jumpSpeed = 5;
                player.GetComponent<PauseManager>().enabled = true;
            }
        }
    }

    void ControllerRun()
    {
        if(Input.GetKey(KeyCode.W)) //키를 누르는 동안
        {
            Debug.Log("KeyDown W");
            di.setInputKeyDown(0, true);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            di.setInputKeyDown(0, false);
        }

        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("KeyDown A");
            di.setInputKeyDown(1, true);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            di.setInputKeyDown(1, false);
        }

        if(Input.GetKey(KeyCode.S))
        {
            Debug.Log("KeyDown S");
            di.setInputKeyDown(2, true);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            di.setInputKeyDown(2, false);
        }

        if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("KeyDown D");
            di.setInputKeyDown(3, true);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            di.setInputKeyDown(3, false);
        }
    }

    public void setIsAccess(bool isAccess)
    {
        this.isAccess = isAccess;
    }

    public bool getIsAccess()
    {
        return isAccess;
    }
}
