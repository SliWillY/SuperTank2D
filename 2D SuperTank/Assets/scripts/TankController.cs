using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviour
{

    PhotonView pw;
    public float benzin =10000f;
    public GameObject benzinBari;

    private Rigidbody2D rb2d;
    private Vector2 movementVector;
    public float maxSpeed = 180;
    public float rotationSpeed = 100;
    public float turretRotationSpeed = 150;
    public Transform turretParent;


    public GameObject bullet;
    public Transform atesNoktasi;
    public float mermiHizi;
    public float sayac;
    public GameObject benzinAl;



    private void Awake()
    {
        
        pw = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
    }





    private void Update()
    {
        
        hareket();
        atesEt();

    }

    //ate� k�sm�
    public void atesEt()
    {
        sayac -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (sayac <= 0)
            {
                GameObject mermi = Instantiate(bullet, atesNoktasi.position, atesNoktasi.rotation);
                mermi.GetComponent<Rigidbody2D>().velocity = atesNoktasi.up * mermiHizi;
                Destroy(mermi, 5f);
                sayac = 1.5f;
            }

        }
    }
    //hareket k�sm�

    void hareket()
    {

        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));


        if (Input.GetKey(KeyCode.W))
        {
            benzinBari.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            benzinBari.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            benzinBari.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            benzinBari.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }

        if (benzin <= 0)
        {
            benzin = 0;
            maxSpeed = 0;
        }

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

    // trigger k�sm�

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camur")
        {
            maxSpeed = 80;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        maxSpeed = 180;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="benzinAl")
        {
            benzin = benzin + 3000;
            Destroy(benzinAl);
            if (benzin>=10000)
            {
                benzin = 10000;
            }
            
        }
    }
}
