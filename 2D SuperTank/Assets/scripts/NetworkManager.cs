using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    [SerializeField] GameObject spawn1;
    [SerializeField] GameObject spawn2;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {


        PhotonNetwork.ConnectUsingSettings();
        /* PhotonNetwork.JoinLobby();
         PhotonNetwork.JoinRoom("oda isim");
         PhotonNetwork.JoinRandomRoom();
         PhotonNetwork.CreateRoom("oda isim", new RoomOptions {MaxPlayers =2,IsOpen=true,IsVisible=true},TypedLobby.Default);
         PhotonNetwork.JoinOrCreateRoom("oda isim", new RoomOptions {MaxPlayers =2,IsOpen=true,IsVisible=true},TypedLobby.Default);
         PhotonNetwork.LeaveLobby();
         PhotonNetwork.LeaveRoom();*/

    }

    /* public override void OnConnected()
     {
         Debug.Log("Ham Baðlantý oldu");
     }*/
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");

        // PhotonNetwork.JoinRoom("oda isim");
        // PhotonNetwork.JoinRandomRoom();

        // PhotonNetwork.CreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom("room name", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Tank_shotgun", transform.position, Quaternion.identity, 0, null);
            
        }
        else
        {
            PhotonNetwork.Instantiate("player2", transform.position, Quaternion.identity, 0, null);
        
        }
        /*
        PhotonNetwork.Instantiate("player", new Vector3(-4,-1), Quaternion.identity,0,null);             
        PhotonNetwork.Instantiate("player2", new Vector3(-3,-1), Quaternion.identity,0,null);
        */
        Debug.Log("joined room.");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("left lobby.");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("left room.");
    }



    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to join room!" + message + " - " + returnCode);

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("failed to join a random room" + message + " - " + returnCode);

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to creat room!" + message + " - " + returnCode);
    }
}

