using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullet : MonoBehaviour
{
    Rigidbody2D rg;
    PhotonView pv;
    float speed;
    float _time;
    float slowDownAmount = 40f;
    Vector3 bulletVelocity;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        speed = Random.Range(18.0f, 22.0f); // Set a random speed for the bullet
    }

    void Update()
    {
        
        transform.Translate(Time.deltaTime * Vector2.up * speed);

        _time += Time.deltaTime;
        if (_time > 0.15f)
        {
            if (pv.IsMine)
            {
                // Slow down the bullet over time
                speed -= slowDownAmount * Time.deltaTime;
                if (speed < 0)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
    





