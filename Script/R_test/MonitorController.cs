using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviourPun
{
    public GameObject[] socket = new GameObject[3];
    public GameObject[] connectGameObject = new GameObject[3];
    public int[] connectObjectID = new int[3];
    public GameObject batteryDevice;
    public GameObject activeScreen;
    bool[] connect = new bool[3];
    HPhysic.Connector[] connectCable = new HPhysic.Connector[3];

    //public bool successCompile;

    // Update is called once per frame
    void Update()
    {
        if(batteryDevice.GetComponent<BatteryDeviceController>().equipBatCount > 0)
        {
            photonView.RPC("MonitorActive", RpcTarget.All, true);
        }
        else
        {
            photonView.RPC("MonitorActive", RpcTarget.All, false);
        }

        for (int i = 0; i < 3; i++)
        {
            connectGameObject[i] = GetConnectObject(socket[i]);
        }
        //케이블로 물리적으로 연결된 오브젝트를 가져옴
    }

    public GameObject GetConnectObject(GameObject _socket)
    {
        GameObject _connectGameObject = null;
        HPhysic.Connector _connectObject;

        if (_socket.GetComponent<HPhysic.Connector>().ConnectedTo != null)
        {
            _connectObject = _socket.GetComponent<HPhysic.Connector>().ConnectedTo;
            _connectGameObject = _connectObject.transform.parent.gameObject.transform.GetChild(1).gameObject; 
            
            _connectObject = _connectGameObject.GetComponent<HPhysic.Connector>().ConnectedTo; 
            _connectGameObject = _connectObject.transform.parent.gameObject;

            //Debug.Log("_connectGameObject = " + _connectGameObject);
        }     

        return _connectGameObject;
    }

    [PunRPC]
    public void MonitorActive(bool b)
    {
        activeScreen.SetActive(b);
    }

    [PunRPC]
    public void ConnectObjectSync(int[] ID)
    {
        for (int i = 0; i < 3; i++)
        {
            connectGameObject[i] = PhotonView.Find(ID[i]).gameObject;
        }
    }
}
