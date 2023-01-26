using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpSpawner : MonoBehaviourPunCallbacks
{

    public GameObject[] powerUpPrefab; 
    public float spawnDelay = 30f; // The time in seconds before the power-up spawns again
    private float spawnTimer = 0f; // The timer for the spawn delay
    private bool powerUpTaken = false; // A flag to check if the power-up has been taken
    PhotonView pv;

    public int powerUpRandom;
    public int PowerUpType;

    private GameObject spawnedObject;

    void Start()
    {
        pv = GetComponent<PhotonView>();   
        
        if (PhotonNetwork.IsMasterClient) // Check if the current client is the master client
        {
            powerUpRandom = Random.Range(0, powerUpPrefab.Length);
            PhotonNetwork.Instantiate(powerUpPrefab[powerUpRandom].name, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        //Debug.Log(spawnTimer);

        if (PhotonNetwork.IsMasterClient) // Check if the current client is the master client
        {
            if (powerUpTaken)
            {
                spawnTimer += Time.deltaTime; // Increment the spawn timer

                if (spawnTimer >= spawnDelay) // Check if the spawn timer has reached the spawn delay
                {
                    spawnTimer = 0f; 
                    powerUpTaken = false; 

                    powerUpRandom = Random.Range(0, powerUpPrefab.Length);

                    PhotonNetwork.Instantiate(powerUpPrefab[Random.Range(0, powerUpPrefab.Length)].name, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void TakePowerUp()
    {
        if(powerUpTaken)
        {
            return;
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            // Inform the master client that a power-up has been taken
            pv.RPC("PowerUpTaken", RpcTarget.MasterClient);
        }
        else
        {
            powerUpTaken = true;
        }
    }

    [PunRPC]
    private void PowerUpTaken()
    {
        powerUpTaken = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

