using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float destroyTime;
    private void Awake()
    {
        
    }
    private void Update()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {        
        yield return new WaitForSeconds(destroyTime); // wait for the reload time
        PhotonNetwork.Destroy(gameObject);
    }
}
