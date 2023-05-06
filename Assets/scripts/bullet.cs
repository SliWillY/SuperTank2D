using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public float bulletSpeed;
    public float bulletDamage;
    public float damageMultiplayer;
    public float timeToMultiplayDamage;
    public bool slowDownBullet;
    public float bulletSlowDownAmount;
    [Range(0, 5)] public int bulletSpeedRandomness;
    [Range(0, 1)] public float timeToStartSlowDown;

    public GameObject bulletObject;
}
