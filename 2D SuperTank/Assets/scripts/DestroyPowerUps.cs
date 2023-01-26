using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyPowerUps : MonoBehaviourPunCallbacks
{
    private bool powerUpTaken = false;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpTaken)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void PowerUpTaken()
    {
        powerUpTaken = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                pv.RPC("PowerUpTaken", RpcTarget.MasterClient);
            }
            else
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
