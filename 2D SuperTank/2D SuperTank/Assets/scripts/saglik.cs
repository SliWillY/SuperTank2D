using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saglik : MonoBehaviour
{

    public int sagl�k = 100;
    public Slider canBari;
    
    

    void Start()
    {
        
    }

    void Update()
    {
        canBari.value = sagl�k;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            sagl�k = sagl�k - 20;
            if (sagl�k <=0)
            {
                
                Destroy(this.gameObject);
            }
        }
    }
}
