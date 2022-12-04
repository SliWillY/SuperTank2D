using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saglik : MonoBehaviour
{

    public int saglýk = 100;
    public Slider canBari;
    
    

    void Start()
    {
        
    }

    void Update()
    {
        canBari.value = saglýk;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            saglýk = saglýk - 20;
            if (saglýk <=0)
            {
                
                Destroy(this.gameObject);
            }
        }
    }
}
