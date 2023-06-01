using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviourPun
{
    [SerializeField] GameObject RestartPopup;
    [SerializeField] GameObject QuitPopup;
    [SerializeField] GameObject pauseMenu;

    public void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                this.GetComponent<ChController>().enabled = false;
            }
            else
            {
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                this.GetComponent<ChController>().enabled = true;
            }
        }
    }

    public void ResumeButtonDown()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        this.GetComponent<ChController>().enabled = true;
        Cursor.visible = false;
    }
        
    public void RestartButtonDown()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        RestartPopup.SetActive(true);
    }

    public void RestartPopupYesButtonDown()
    {
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButtonDown()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        QuitPopup.SetActive(true);
    }

    [PunRPC]
    public void RPCQuitPopupYesButtonDown()
    {
        photonView.RPC("QuitPopupYesButtonDown", RpcTarget.All);
    }

    [PunRPC]
    public void QuitPopupYesButtonDown()
    {        
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.LeaveRoom();
    }
}
