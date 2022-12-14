using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rg;
    float speed = 15f;

    void Update()
    {
       
       transform.Translate(Time.deltaTime * Vector2.up * speed);
        
        
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="player2")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag!=null && collision.gameObject.tag!="camur")
        {
            Destroy(this.gameObject);

        }
    }
    */
}
