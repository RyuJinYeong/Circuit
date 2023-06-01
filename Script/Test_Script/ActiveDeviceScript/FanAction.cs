using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAction : MonoBehaviourPun
{
    /// <summary>
    /// fan의 동작
    /// </summary>
    [SerializeField] private DeviceInfo di;

    public GameObject fanObject; //돌아갈 팬 오브젝트
    public float speed;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        di = GetComponent<DeviceInfo>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (di.getIsOn())
        {
            isOn = di.getIsOn();
            photonView.RPC("SyncStateRPC", RpcTarget.Others, di.getIsOn(), di.getValue());
        }
        
        if (isOn)
            fanRotate(speed);
    }

    void fanRotate(float _speed)
    {
        fanObject.transform.Rotate(0, _speed * Time.deltaTime, 0);
    }


    [PunRPC]
    void SyncState(bool isOn, float speed)
    {
        this.isOn = isOn;
        this.speed = speed;
    }
}