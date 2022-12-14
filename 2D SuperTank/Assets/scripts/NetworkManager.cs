using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    PhotonView pw;

    private void Awake()
    {
        pw = GetComponent<PhotonView>();
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
         Debug.Log("Ham Bağlantı oldu");
     }*/
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server'e Bağlanıldı.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye bağlanıldı.");

        // PhotonNetwork.JoinRoom("oda isim");
        // PhotonNetwork.JoinRandomRoom();

        // PhotonNetwork.CreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }


    public override void OnJoinedRoom()
    {
<<<<<<< HEAD
        if (pw.IsMine)
        {
            PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("player2", Vector3.zero, Quaternion.identity);
        }

           
            Debug.Log("Odaya Girildi.");
=======
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("player", new Vector3(-4, -1), Quaternion.identity, 0, null);
        }
        else
        {
            PhotonNetwork.Instantiate("player2", new Vector3(-3, -1), Quaternion.identity, 0, null);
        }
        /*
        PhotonNetwork.Instantiate("player", new Vector3(-4,-1), Quaternion.identity,0,null);             
        PhotonNetwork.Instantiate("player2", new Vector3(-3,-1), Quaternion.identity,0,null);
        */
        Debug.Log("Odaya Girildi.");
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çıkıldı.");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çıkıldı.");
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
        Debug.Log("Oda oluşturulamadı." + message + " - " + returnCode);
    }
}

