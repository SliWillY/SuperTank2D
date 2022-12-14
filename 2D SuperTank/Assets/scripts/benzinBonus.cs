using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class benzinBonus : MonoBehaviour
{
    gameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<gameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" || collision.gameObject.tag == "player2")
        {
            gameManager.benzin = gameManager.benzin + 3000;
            Destroy(this.gameObject);
            if (gameManager.benzin >= 10000)
            {
                gameManager.benzin = 10000;
            }

        }
    }

}
