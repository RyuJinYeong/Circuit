using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDeviceDoorController : MonoBehaviourPun
{
    public GameObject[] childBat = new GameObject[3];

    [PunRPC]
    public void RPCBatSetActive(bool b, int n)
    {
        childBat[n].SetActive(b);
    }

    public void BatSetActive(bool b, int n)
    {
        photonView.RPC("RPCBatSetActive", RpcTarget.All,b,n);
    }
}
