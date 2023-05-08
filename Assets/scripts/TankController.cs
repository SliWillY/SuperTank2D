using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankController : MonoBehaviour
{

    PhotonView pv;
    [SerializeField] float Fuel = 10000f;
    [SerializeField] GameObject fuelBar;
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
    [SerializeField] private int numShots = 8; // Number of shots to fire
    private float spreadAngle = 30.0f; // Spread angle for the shots

    bool rateOfFireDecreased = false;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();
        fuelBar = GameObject.FindGameObjectWithTag("GasBar");
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

    //fire command
    public void Fire()
    {
        fireRate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate <= 0)
            {
                //GameObject _bullet = PhotonNetwork.Instantiate("Bulleto", firePos.position, firePos.rotation);
                for (int i = 0; i < numShots; i++)
                {
                    // Calculate a random direction for the shot
                    float angle = Random.Range(-spreadAngle * 0.5f, spreadAngle * 0.5f);
                    Quaternion rot = Quaternion.Euler(0, 0, angle);

                    // Instantiate the bullet in the random direction
                    GameObject _bullet = PhotonNetwork.Instantiate("Bulleto", firePos.position, rot * firePos.rotation);
                    fireRate = 0.5f;
                }
                
                /*if (rateOfFireDecreased == true)
                {
                    fireRate = 0.2f;

                    StartCoroutine(bulletPower());
                }*/
            }

        }
    }
    //movment command

    void Move()
    {

        rigidBody.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rigidBody.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            fuelBar.transform.localScale = new Vector2(Fuel / 10000, 1);
            Fuel -= Time.deltaTime * 120;

            animator.SetBool("isForward", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isForward", false);

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)){
            fuelBar.transform.localScale = new Vector2(Fuel / 10000, 1);
            Fuel -= Time.deltaTime * 120;

            animator.SetBool("isBack", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isBack", false);

        }

        //Vector3 playerVelocity = rigidBody.velocity;
        float playerSpeed = rigidBody.velocity.magnitude;
        Debug.Log(playerSpeed);

        if (Fuel <= 0)
        {
            Fuel = 0;
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
        if (collision.gameObject.CompareTag("mud"))
        {
            maxSpeed = 140;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fuel"))
        {
            Fuel += 3000;
            if (Fuel >= 10000)
            {
                Fuel = 10000;
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
