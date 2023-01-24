using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernamelnput;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private AudioClip _compressClip, _unconpressClip;
    [SerializeField] private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnC1ickConnect()
    {
        if (usernamelnput.text.Length >= 1)
        {
            _source.PlayOneShot(_compressClip);
            PhotonNetwork.NickName = usernamelnput.text;
            buttonText.text = "Connecting... ";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            _source.PlayOneShot(_unconpressClip);
        }

    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
