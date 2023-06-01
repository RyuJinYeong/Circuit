using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviourPun
{
    public bool isClear = false;
    [SerializeField] GameObject clearPopup;

    void Awake()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        if (isClear)
        {
            photonView.RPC("clearPopUpOn", RpcTarget.All, isClear);
            clearPopup.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PhotonNetwork.LoadLevel(0);
                PhotonNetwork.LeaveRoom();
            }
        }
    }

    [PunRPC]
    public void clearPopUpOn(bool isC)
    {
        this.isClear = isC;
    }
}
