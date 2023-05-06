using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraPhotonView : MonoBehaviour
{
    private PhotonView pv;
    private void Start()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine)
        {
            pv.RPC("turnOffCamera",RpcTarget.All);
        }
    }
}
