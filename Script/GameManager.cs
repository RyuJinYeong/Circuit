using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject player1;
        public GameObject player2;
        public GameObject SpawnPoint1;
        public GameObject SpawnPoint2;

        void Start()
        {
            if (ChController.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient)
                {

                    PhotonNetwork.Instantiate(this.player1.name, SpawnPoint1.transform.position, Quaternion.identity, 0);
                }
                else
                {
                    PhotonNetwork.Instantiate(this.player2.name, SpawnPoint2.transform.position, Quaternion.identity, 0);
                }
            }
        }

        #region Photon Callbacks


        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
    }
}