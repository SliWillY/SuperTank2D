using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class Controller : MonoBehaviour
{
    public Tank tankScriObj;
    public HealthSystem healthSystem;

    [SerializeField] private GameObject turret;
    [SerializeField] private Transform bulletFirePos;

    PlayerControls playerInput;
    Rigidbody2D rb;
    PhotonView pv;
    CharacterController charaterController;
    GameObject virtualCameraObj;
    CinemachineVirtualCamera virtualCamera;

    Vector2 currentMovmentInput;
    Vector2 currentMovment;

    Vector2 currentTurretInput;
    Vector2 currentTurret;

    bool isMovmentPressed;
    bool isFirePressed;
    bool isBoostPressed;
    bool isAbilityPressed;
    bool isUltimatePressed;

    float tankCurrentHealth;

    float nextFireTime; // the time when the next bullet can be fired
    int bulletsLeft; // the number of bullets left in the magazine
    bool reloading; // flag to check if reloading

    private float tankSpeed;
    private float tankMaxHealth;
    private float tankRotSpeed;
    private float turretRotSpeed;

    private float fireRate;
    private int magazineSize;
    private float reloadTime;
    private bool IsOneShot;
    private float bulletSpreadAngle;
    private int bulletAmountPerShot;

    private GameObject bulletObject;

    public void Awake()
    {
        //Cinamachine Camera situp
        virtualCameraObj = GameObject.FindGameObjectWithTag("VirtualCamera");
        virtualCamera = virtualCameraObj.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = this.transform;

        //GetComponent
        playerInput = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
        charaterController = GetComponent<CharacterController>();

        bulletsLeft = magazineSize;
        nextFireTime = 0f;
        reloading = false;

        //Assign scribtableObj values to local
        tankSpeed = tankScriObj.speed;
        tankMaxHealth = tankScriObj.maxHealth;
        tankRotSpeed = tankScriObj.rotSpeed;
        turretRotSpeed = tankScriObj.turretRotSpeed;
        fireRate = tankScriObj.fireRate;
        magazineSize = tankScriObj.magazineSize;
        reloadTime = tankScriObj.reloadTime;
        IsOneShot = tankScriObj.isOneShot;
        bulletSpreadAngle = tankScriObj.bulletSpreadAngle;
        bulletAmountPerShot = tankScriObj.bulletAmountPerShot;
        bulletObject = tankScriObj.bulletObject;

        //Input Invoke
        if (pv.IsMine)
        {
            playerInput.Player.Move.started += OnMove;
            playerInput.Player.Move.canceled += OnMove;
            playerInput.Player.Move.performed += OnMove;
            playerInput.Player.Fire.started += OnFire;
            playerInput.Player.Fire.canceled += OnFire;
            playerInput.Player.Fire.performed += OnFire;
            playerInput.Player.Boost.started += OnBoost;
            playerInput.Player.Boost.canceled += OnBoost;
            playerInput.Player.Ability.started += OnAbility;
            playerInput.Player.Ability.canceled += OnAbility;
            playerInput.Player.Ultimate.started += OnUltimate;
            playerInput.Player.Ultimate.canceled += OnUltimate;
        }
    }

    private void Update()
    {
        //rb.velocity = Time.fixedDeltaTime * currentMovment;

        //RigidBody Move
        //rb.velocity = (Vector2)transform.up * currentMovment.y * tankSpeed * Time.fixedDeltaTime * 5;
        //rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -currentMovment.x * tankRotSpeed * Time.fixedDeltaTime));


        //Tranform.Translate
        //transform.Translate(currentMovment * Time.deltaTime, Space.World);

        HandleRotation();
        HandleFire();
        //CharacterController
        charaterController.Move(currentMovment * Time.deltaTime);

        
    }

    void HandleRotation()
    {
        Vector2 positionToLookAt;
        Vector2 turretPositionToLookAt;
        // the change in  posintion our character should point to
        positionToLookAt.x = currentMovmentInput.x;
        positionToLookAt.y = currentMovmentInput.y;

        turretPositionToLookAt = currentTurretInput;

        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;
        Quaternion turretCurrentRotation = turret.transform.rotation;

        //Body Rotation handler
        if (isMovmentPressed /*|| !isMovmentPressed*/)
        {
            // create a new rotation based on where the player is currently pressing

            //Transform Rotation
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, tankRotSpeed * Time.fixedDeltaTime);

            //RigidBody Rotation
            //rb.angularVelocity = rotationFactorPerFrame * Mathf.Atan2(positionToLookAt.y, positionToLookAt.x) * Mathf.Rad2Deg;
        }

        //Turret Rotation handler
        if (isFirePressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, turretPositionToLookAt);
            turret.transform.rotation = Quaternion.Slerp(turretCurrentRotation, targetRotation, turretRotSpeed * Time.fixedDeltaTime);
        }
    }

    void HandleFire()
    {
        if (reloading)
        {
            return;
        }
        if (isFirePressed)
        {
            if (Time.time >= nextFireTime && bulletsLeft > 0)
            {
                if (IsOneShot)
                {
                    Instantiate(bulletObject, bulletFirePos.position, bulletFirePos.rotation);
                }
                else
                {
                    for (int i = 0; i < bulletAmountPerShot; i++)
                    {
                        // Calculate a random direction for the shot
                        float angle = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
                        Quaternion rot = Quaternion.Euler(0, 0, angle);

                        // Instantiate the bullet in the random direction
                        Instantiate(bulletObject, bulletFirePos.position, rot * bulletFirePos.rotation);
                        //fireRate = 0.5f;
                    }
                }               
                nextFireTime = Time.time + fireRate;
                bulletsLeft--;
            }

        }

        if (bulletsLeft <= 0 && !reloading) // check if the magazine is empty and not currently reloading
        {
            StartCoroutine(Reload()); // start the reload coroutine
        }
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        currentMovmentInput = ctx.ReadValue<Vector2>();
        currentMovment.x = currentMovmentInput.x * tankSpeed; 
        currentMovment.y = currentMovmentInput.y * tankSpeed;

        isMovmentPressed = currentMovmentInput.x != 0 || currentMovmentInput.y != 0;
        
    }
    void OnFire(InputAction.CallbackContext ctx)
    {
        currentTurretInput = ctx.ReadValue<Vector2>();
        currentTurret = currentTurretInput;

        isFirePressed = currentTurretInput.x != 0 || currentTurretInput.y != 0;

    }
    void OnBoost(InputAction.CallbackContext ctx)
    {

    }
    void OnAbility(InputAction.CallbackContext ctx)
    {

    }
    void OnUltimate(InputAction.CallbackContext ctx)
    {

    }

    IEnumerator Reload()
    {
        reloading = true; // set the reloading flag
        yield return new WaitForSeconds(reloadTime); // wait for the reload time
        bulletsLeft = magazineSize; // set the bullets left to the magazine size
        reloading = false; // set the reloading flag
    }



    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}
