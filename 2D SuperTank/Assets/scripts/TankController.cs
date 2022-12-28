using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviour
{

    PhotonView pv;
    [SerializeField] float Feul = 10000f;
    [SerializeField] GameObject feulBar;
    [SerializeField] Animator animator;

    private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    public float maxSpeed = 250;
    [SerializeField] float speedAftertakeFeul = 250;
    public float rotationSpeed = 130;
    public float turretRotationSpeed = 150;
    [SerializeField] Transform turretParent;
    [SerializeField] private GameObject cinemachineCam;

    public Transform firePos;
    public float bulletSpeed;
    public float fireRate;

    bool rateOfFireDecreased = false;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();
        feulBar = GameObject.FindGameObjectWithTag("GasBar");
        animator = animator.gameObject.GetComponent<Animator>();
        if (pv.IsMine)
        {
            cinemachineCam.SetActive(true);
        }
    }

    private void Start()
    {
        
        if (pv.IsMine)
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
        
    }





    private void Update()
    {
        if (pv.IsMine)
        {
            Move();
            Fire();
        }     
    }

    //ateþ kýsmý
    public void Fire()
    {
        fireRate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate <= 0)
            {
                GameObject _bullet = PhotonNetwork.Instantiate("Bulleto", firePos.position, firePos.rotation);
                fireRate = 1.5f;

                if (rateOfFireDecreased == true)
                {
                    fireRate = 0.5f;

                    StartCoroutine(bulletPower());
                }
            }

        }
    }
    //hareket kýsmý

    void Move()
    {

        rigidBody.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rigidBody.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            feulBar.transform.localScale = new Vector2(Feul / 10000, 1);
            Feul -= Time.deltaTime * 120;

            animator.SetBool("isForward", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isForward", false);

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)){
            feulBar.transform.localScale = new Vector2(Feul / 10000, 1);
            Feul -= Time.deltaTime * 120;

            animator.SetBool("isBack", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isBack", false);

        }


        if (Feul <= 0)
        {
            Feul = 0;
            maxSpeed = 50f;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("camur"))
        {
            maxSpeed = 140;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("benzinAl"))
        {
            Feul += 3000;
            if (Feul >= 10000)
            {
                Feul = 10000;
            }
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("bulletPower"))
        {
            rateOfFireDecreased = true;
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        maxSpeed = speedAftertakeFeul;
    }
    IEnumerator bulletPower()
    {
        yield return new WaitForSeconds(3f);
        rateOfFireDecreased = false;

        fireRate = 1.5f;
    }
}
