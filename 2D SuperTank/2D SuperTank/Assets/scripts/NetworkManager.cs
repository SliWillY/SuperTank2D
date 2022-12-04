using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{

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
        Debug.Log("Server'e Baðlanýldý.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye baðlanýldý.");

        // PhotonNetwork.JoinRoom("oda isim");
        // PhotonNetwork.JoinRandomRoom();

        // PhotonNetwork.CreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi.");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çýkýldý.");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çýkýldý.");
    }



    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Random Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Oda oluþturulamadý." + message + " - " + returnCode);
    }
}

