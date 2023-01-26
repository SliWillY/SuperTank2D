using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class HealthSystem : MonoBehaviour
{
    public Tank tankScriObj;
    public BulletManager bulletManager; 
    public Controller controller;

    PlayerSpawner spawner;
    GameObject healthBarObj;
    Slider healthBar;
    Image healthBarFill;
    [SerializeField] Gradient gradient;
    PhotonView pv;

    float tankMaxHealth;
    float tankCurrentHealth;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) { return; }

        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlayerSpawner>();
        healthBarObj = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = healthBarObj.GetComponent<Slider>();
        healthBarFill = healthBarObj.transform.Find("Fill").GetComponent<Image>();

        tankMaxHealth = tankScriObj.maxHealth;
        tankCurrentHealth = tankMaxHealth;

        healthBar.maxValue = tankMaxHealth;
        healthBar.value = tankMaxHealth;

        healthBarFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SetMaxHealth()
    {
        healthBarFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SetHealth(float newHealth)
    {
        tankCurrentHealth -= newHealth;
        healthBar.value = tankCurrentHealth;
        healthBarFill.color = gradient.Evaluate(healthBar.normalizedValue);

        if (tankCurrentHealth <= 0) 
        {
            spawner.Respawn();
            PhotonNetwork.Destroy(this.gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!pv.IsMine) { return; }

        if (controller.PowerUpShield)
        {
            return;
        }
        if (other.CompareTag("bullet"))
        {
            controller.PlaySoundLocally(2);

            try
            {
                float damage = other.GetComponent<BulletManager>().bulletDamage;
                Debug.Log(damage);

                SetHealth(damage);
            }
            catch
            {
                SetHealth(-5f);
            }

            
        }
    }
}
