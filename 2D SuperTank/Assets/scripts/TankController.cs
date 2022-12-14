using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviour
{

    PhotonView pw;
<<<<<<< HEAD
    gameManager gameManager;
=======
    public float benzin =10000f;
    public GameObject gasBar;
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28

    private Rigidbody2D rb2d;
    private Vector2 movementVector;
    public float maxSpeed = 180;
    public float rotationSpeed = 100;
    public float turretRotationSpeed = 150;
    public Transform turretParent;

    private Camera mainCamera;
    public GameObject bullet;
    public Transform atesNoktasi;
    public float mermiHizi;
    public float sayac;
    public GameObject benzinAl;



    private void Awake()
    {

        pw = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        gameManager = FindObjectOfType<gameManager>();
=======
        gasBar = GameObject.FindGameObjectWithTag("GasBar");
        mainCamera = Camera.main;
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
    }

    private void Start()
    {
        /*
        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-4, -1);
            }
            else if(!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-3, 0);
            }
        }
        */
    }





    private void Update()
    {
<<<<<<< HEAD

=======
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
        if (pw.IsMine)
        {
            hareket();
            atesEt();
<<<<<<< HEAD
        }
=======
        }     
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
    }

    //ateþ kýsmý
    public void atesEt()
    {
        sayac -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (sayac <= 0)
            {
                GameObject mermi = Instantiate(bullet, atesNoktasi.position, atesNoktasi.rotation);
                //mermi.GetComponent<Rigidbody2D>().velocity = atesNoktasi.up * mermiHizi;
                //Destroy(mermi, 5f);
                sayac = 1.5f;
            }

        }
    }
    //hareket kýsmý

    void hareket()
    {

        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
<<<<<<< HEAD
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.deltaTime));
=======
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        /*
        Vector3 mouseWorldPos = mainCamera.WorldToScreenPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        HandleTurretMovement(mouseWorldPos);
        */
        if (Input.GetKey(KeyCode.W))
        {
            gasBar.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gasBar.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            gasBar.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gasBar.transform.localScale = new Vector2(benzin / 10000, 1);
            benzin = benzin - 1;
        }

        if (benzin <= 0)
        {
            benzin = 0;
            maxSpeed = 50f;
        }

>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        if (pw.IsMine)
        {
            this.movementVector = movementVector;

        }

    }


    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        if (pw.IsMine)
        {
            var turretDirection = (Vector3)pointerPosition - turretParent.position;
            var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
            var rotationStep = turretRotationSpeed * Time.deltaTime;
            turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationStep);
        }
        
    }

    // trigger kýsmý

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camur")
        {
            maxSpeed = 80;
        }

        if (collision.gameObject.tag == "benzinAl")
        {
            benzin =+ 3000;
            if (benzin >= 10000)
            {
                benzin = 10000;
            }

            PhotonNetwork.Destroy(collision.gameObject);

        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        maxSpeed = 180;
    }
<<<<<<< HEAD

   
=======
>>>>>>> ca5d8644358bde059734326534b2caba8f37dd28
}
