using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class HealthSystem : MonoBehaviour
{
    public Tank tankScriObj;

    GameObject healthBarObj;
    Slider healthBar;
    Image healthBarFill;
    [SerializeField] Gradient gradient;
    PhotonView pv;

    float tankMaxHealth;
    float tankCurrentHealth;

    private void Awake()
    {

        healthBarObj = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = healthBarObj.GetComponent<Slider>();
        healthBarFill = healthBarObj.transform.Find("Fill").GetComponent<Image>();
        pv = GetComponent<PhotonView>();

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

        if (!pv.IsMine) { return; }
        if (tankCurrentHealth <= 0) { PhotonNetwork.Destroy(gameObject); }
    }
}
