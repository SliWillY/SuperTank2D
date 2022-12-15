using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullet : MonoBehaviour
{
    Rigidbody2D rg;
    PhotonView pv;
    float speed = 15f;
    float _time;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    void Update()
    {      
        transform.Translate(Time.deltaTime * Vector2.up * speed); 
        
        _time += Time.deltaTime;
        if(_time > 5f)
        {
            if (pv.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
