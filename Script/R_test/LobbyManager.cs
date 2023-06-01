using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyManager: MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    public static int ClearStageNum = 0;

    public int selectStageNum;

    public GameObject joinPanel;
    public GameObject roomPanel;
    public GameObject SelectMapImage;

    [SerializeField] GameObject SettingPopup;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject lobbyPanel;
    [SerializeField] GameObject createPanel;    
    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Transform RoomList;
    [SerializeField] GameObject AboutUs;
    [SerializeField] GameObject MapContentList;
    [SerializeField] GameObject MapSelectCanvas;
    [SerializeField] Text MapInfo;
    [SerializeField] Image MapImageInfo;
    [SerializeField] Image BackupMapImage;
    



    // Start is called before the first frame update
    void Start()
    {
        nickName.text = null;
        Instance = this;
    }

    private void PanelDisable()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    public void ToMainPanel()
    {
        nickName.text = null;
        PhotonNetwork.NickName = null;
        PanelDisable();
        mainPanel.SetActive(true);
    }

    public void ToControlPanel()
    {
        PanelDisable();
        controlPanel.SetActive(true);
    }

    public void ToLobbyPanel()
    {
        if (string.IsNullOrEmpty(nickName.text))
            nickName.text = PhotonNetwork.NickName;

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            if(string.IsNullOrEmpty(nickName.text))
                return;
        }
        PanelDisable();
        lobbyPanel.SetActive(true);

        foreach (Transform child in RoomList)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToCreatePanel()
    {
        PanelDisable();
        createPanel.SetActive(true);
    }

    public void ToJoinPanel()
    {
        PanelDisable();
        joinPanel.SetActive(true);
    }

    public void ToRoomPanel()
    {
        PanelDisable();
        roomPanel.SetActive(true);
    }

    public void JoinRoom()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        
        roomPanel.SetActive(true);
    }

    public void LeaveRoom()
    {
        MapInfo.text = "LEVEL MAP_??";
        MapImageInfo.sprite = BackupMapImage.GetComponent<Image>().sprite;
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void ExitButtonDown()
    {
        Application.Quit();
    }

    public void AboutUsButtonDown()
    {
        AboutUs.SetActive(true);
    }

    public void AboutUsExit()
    {
        AboutUs.SetActive(false);
    }

    public void LobbyMapSelectButtonDown()
    {
        MapSelectCanvas.SetActive(true);
    }

    public void MapBackButtonDown()
    {
        MapSelectCanvas.SetActive(false);
    }

    public void MapListSelect()
    {
        selectStageNum = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex(); // 이벤트가 발생한 버튼의 형제 인덱스를 반환

        Debug.Log(selectStageNum);
    }

    public void SettingButtonDown()
    {
        SettingPopup.SetActive(true);
    }


    [PunRPC]
    public void MapSelectButtonDown(int _selectStageNum)
    {
        if (_selectStageNum > ClearStageNum)
            return;
        SelectMapImage = MapContentList.transform.GetChild(_selectStageNum).GetChild(0).gameObject; // 선택된 맵의 이미지를 반환
        Debug.Log(SelectMapImage);
        Launcher.Instance.stageNum = _selectStageNum + 1;
        MapInfo.text = "LEVEL MAP_" + _selectStageNum + 1;
        MapImageInfo.sprite = SelectMapImage.GetComponent<Image>().sprite;
        
        
        MapSelectCanvas.SetActive(false);
    }

    [PunRPC]
    public void RpcButtonDown()
    {
        photonView.RPC("MapSelectButtonDown", RpcTarget.All, selectStageNum);
    }
}
