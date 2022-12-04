using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movementVector;
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    public float turretRotationSpeed = 150;
    public Transform turretParent;


    public GameObject bullet;
    public Transform atesNoktasi;
    public float mermiHizi;
    public float sayac;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //ateþ kýsmý
    public void atesEt()
    {
        sayac -= Time.fixedDeltaTime;
        if (Input.GetMouseButton(0))
        {
            if (sayac<=0)
            {
                GameObject mermi = Instantiate(bullet, atesNoktasi.position,atesNoktasi.rotation);
                mermi.GetComponent<Rigidbody2D>().velocity = atesNoktasi.up * mermiHizi;
                Destroy(mermi,5f);
                sayac = 1.5f;
            }
           
        }
    }

   

    private void FixedUpdate()
    {
        hareket();
        atesEt();
    }


    //hareket kýsmý

    void hareket()
    {
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }


    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - turretParent.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationStep);
    }
}
 