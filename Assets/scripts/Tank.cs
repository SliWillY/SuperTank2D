using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTank", menuName = "Tank")]
public class Tank : ScriptableObject
{
    public float speed;
    public float maxHealth;
    public float rotSpeed;
    public float turretRotSpeed;

    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public bool isOneShot;
    public float bulletSpreadAngle;
    public int bulletAmountPerShot;
}