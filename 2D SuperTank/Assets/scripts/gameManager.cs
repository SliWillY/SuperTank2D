using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject benzinBari;
    public float benzin = 10000f;
    TankController tankController;
    private void Awake()
    {
        tankController = FindObjectOfType<TankController>();
        benzin = 10000f;

    }

    void Start()
    {
        
    }

    void Update()
    {
        benzinBariKontrol();
    }

    public void benzinBariKontrol()
    {
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
            tankController.maxSpeed = 0;
        }
    }

}
