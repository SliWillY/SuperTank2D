using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="player2")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag!=null)
        {
            Destroy(this.gameObject);

        }
    }
}
