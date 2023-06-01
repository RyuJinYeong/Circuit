using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    RoomInfo info;

    public void Setup(RoomInfo _info) 
    {
        info = _info;
        text.text = _info.Name + " [" + _info.PlayerCount+"/"+_info.MaxPlayers+"]";
    }

    public void OnClick()
    {
        if(info.PlayerCount >= info.MaxPlayers)
        {
            return;
        }

        LobbyManager.Instance.JoinRoom();
        Launcher.Instance.JoinRoom(info);
    }
}
