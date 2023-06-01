using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDeviceController : MonoBehaviourPun
{
    [SerializeField] GameObject ConnectObject;
    public bool isUp = true;
    Animator anim;
    
    public void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void Update()
    {
        anim.SetBool("isUp", isUp);

        if (!isUp && ConnectObject.CompareTag("Door"))
        {
            ConnectObject.GetComponent<Collider>().enabled = true;
        }
        else if (isUp && ConnectObject.CompareTag("Door"))
        {
            ConnectObject.GetComponent<Collider>().enabled = false;
        }
        else if (!isUp && ConnectObject.CompareTag("Elevator2"))
        {
            ConnectObject.GetComponent<DotTeam.HSK.DotHskMov>().enabled = true;
        }
        else if (isUp && ConnectObject.CompareTag("Elevator2"))
        {
            ConnectObject.GetComponent<DotTeam.HSK.DotHskMov>().enabled = false;
        }
    }

    public void isActive()
    {
        IsUpChange();        
    }


    [PunRPC]
    public void IsUpChange()
    {
        Debug.Log("IsUpChange 호출");
        this.photonView.RPC("RpcIsUpChange", RpcTarget.All);
    }

    [PunRPC]
    public void RpcIsUpChange()
    {
        Debug.Log("RpcIsUpChange 호출");
        isUp = !isUp;
        Debug.Log("isUp : " + isUp);
    }
}
