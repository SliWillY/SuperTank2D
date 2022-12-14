using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class saglik : MonoBehaviour
{

    public int health = 100;
    public Slider healthBar;
    private PhotonView pv;



    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        healthBar.value = health;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (!pv.IsMine)
            {
                return;
            }

            pv.RPC("healthdicrease", RpcTarget.All);
            if (health <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    void healthdicrease()
    {
        health = health - 20;
    }
}
