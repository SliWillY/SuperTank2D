using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullet : MonoBehaviour
{
    public Tank tankScriObj;

    PhotonView pv;

    float _time;

    float bulletSpeed;
    float bulletDamage;
    float damageMultiplayer;
    float timeToMultiplayDamage;
    bool slowDownBullet;
    float bulletSlowDownAmount;
    float timeToStartSlowDown;
    int bulletSpeedRandomness;

    private void Awake()
    {
        bulletSpeed = tankScriObj.bulletSpeed;
        bulletDamage = tankScriObj.bulletDamage;
        damageMultiplayer = tankScriObj.damageMultiplayer;
        timeToMultiplayDamage = tankScriObj.timeToMultiplayDamage;
        slowDownBullet = tankScriObj.slowDownBullet;
        bulletSlowDownAmount = tankScriObj.bulletSlowDownAmount;
        timeToStartSlowDown = tankScriObj.timeToStartSlowDown;
        bulletSpeedRandomness = tankScriObj.bulletSpeedRandomness;  

    }
    void Start()
    {
        pv = GetComponent<PhotonView>();
        bulletSpeed = Random.Range(bulletSpeed - bulletSpeedRandomness, bulletSpeed + bulletSpeedRandomness); // Set a random speed for the bullet
    }

    void Update()
    {
        
        transform.Translate(Time.deltaTime * Vector2.up * bulletSpeed);

        if (slowDownBullet)
        {
            if (_time > timeToStartSlowDown)
            {
                /*if (pv.IsMine)
                {
                    // Slow down the bullet over time
                    bulletSpeed -= bulletSlowDownAmount * Time.deltaTime;
                    if (bulletSpeed < 0)
                    {
                        PhotonNetwork.Destroy(gameObject);
                    }
                }*/

                bulletSpeed -= bulletSlowDownAmount * Time.deltaTime;
                
            }
        }
        else
        {

        }

        if (timeToMultiplayDamage >= _time)
        {
            bulletDamage += damageMultiplayer;
        }

        if (bulletSpeed < 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}






