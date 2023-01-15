using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Tank tankScriObj;
    public GameObject healthBarOgj;
    private Slider healthBar;
    public Gradient gradient;
    public Image healthBarFill;

    public float tankMaxHealth;
    public float tankCurrentHealth;
    
    private void Awake()
    {
        healthBar = healthBarOgj.GetComponent<Slider>();

        tankMaxHealth = tankScriObj.maxHealth;

        healthBar.value = tankMaxHealth;
        tankCurrentHealth = tankMaxHealth;
    }

    public void SetMaxHealth()
    {
        healthBarFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SetHealth(float newHealth)
    {
        tankCurrentHealth += newHealth;
        healthBar.value = tankCurrentHealth;
        healthBarFill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}
