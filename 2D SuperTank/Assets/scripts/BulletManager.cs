using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class BulletManager : MonoBehaviour
{
    public Bullet bulletScriObj;

    [SerializeField] private GameObject hitBullet;

    PhotonView pv;

    float _time;

    float bulletSpeed;
    public float bulletDamage;
    float damageMultiplayer;
    float timeToMultiplayDamage;
    bool slowDownBullet;
    float bulletSlowDownAmount;
    float timeToStartSlowDown;
    int bulletSpeedRandomness;

    private void Awake()
    {
        bulletSpeed = bulletScriObj.bulletSpeed;
        bulletDamage = bulletScriObj.bulletDamage;
        damageMultiplayer = bulletScriObj.damageMultiplayer;
        timeToMultiplayDamage = bulletScriObj.timeToMultiplayDamage;
        slowDownBullet = bulletScriObj.slowDownBullet;
        bulletSlowDownAmount = bulletScriObj.bulletSlowDownAmount;
        timeToStartSlowDown = bulletScriObj.timeToStartSlowDown;
        bulletSpeedRandomness = bulletScriObj.bulletSpeedRandomness;  

    }
    void Start()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine) { return; }
        bulletSpeed = UnityEngine.Random.Range(bulletSpeed - bulletSpeedRandomness, bulletSpeed + bulletSpeedRandomness); // Set a random speed for the bullet
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (!pv.IsMine) { return; }

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
        
        if (bulletSpeed < 0)
        {
            BulletDestroying();
            //BulletDestroying(hitBullet);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {     
        if (timeToMultiplayDamage <= _time)
        {
            bulletDamage += damageMultiplayer;
        }

        if (pv.IsMine) { StartCoroutine(Destroy()); }

        

        //BulletDestroying(hitBullet);
    }

    void BulletDestroying()
    {
        //PhotonNetwork.Instantiate("bulletHit_1", transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f); // wait for the time
        BulletDestroying();
    }
}






