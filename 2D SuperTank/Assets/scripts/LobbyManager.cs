using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }
    
    public void OnC1ickCreate()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void OnclickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }
}
