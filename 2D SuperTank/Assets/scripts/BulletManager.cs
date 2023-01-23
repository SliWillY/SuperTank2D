using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class BulletManager : MonoBehaviour
{
    public Bullet bulletScriObj;
    public HealthSystem healthSystem;

    [SerializeField] private GameObject hitBullet;

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
        bulletSpeed = UnityEngine.Random.Range(bulletSpeed - bulletSpeedRandomness, bulletSpeed + bulletSpeedRandomness); // Set a random speed for the bullet
    }

    void Update()
    {
        _time += Time.deltaTime;

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
        if (other.CompareTag("Player"))
        {
            if (timeToMultiplayDamage <= _time)
            {
                bulletDamage += damageMultiplayer;
            }

            healthSystem = other.GetComponent<HealthSystem>();
            if(healthSystem = null) { throw new NullReferenceException("Noun healthsystem found!"); }
            other.GetComponent<HealthSystem>().SetHealth(bulletDamage);

        }

        BulletDestroying();

        //BulletDestroying(hitBullet);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletDestroying();
    }
    void BulletDestroying()
    {
        if (!pv.IsMine) { return;}

        PhotonNetwork.Instantiate("bulletHit_1", transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
}






