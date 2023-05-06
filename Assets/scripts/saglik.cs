using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class healthSystem : MonoBehaviour
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
            if (pv.IsMine)
            {
                pv.RPC("healthdicrease", RpcTarget.AllViaServer);

                if (health <= 0)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }

            //StartCoroutine(waitSeconds());

            //if (collision.gameObject.GetComponent<PhotonView>().IsMine)
            //{
                //PhotonNetwork.Destroy(collision.gameObject);
            //}
        }
    }

    [PunRPC]
    void healthdicrease()
    {
        health -= 20;
    }

    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
