using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float destroyTime;
    PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (pv.IsMine) { return; }
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {        
        yield return new WaitForSeconds(destroyTime); // wait for the time
        PhotonNetwork.Destroy(gameObject);
    }
}
