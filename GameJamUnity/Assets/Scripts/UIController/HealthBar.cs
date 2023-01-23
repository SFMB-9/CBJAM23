using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private  TextMeshProUGUI healthText;
    [SerializeField] private Image BloodImage;

    private void OnEnable()
    {
        Health.OnDamage += UpdateHealth;
        Health.OnHeal += UpdateHealth;
    }
    
    private void OnDisable()
    {
        Health.OnDamage -= UpdateHealth;
        Health.OnHeal -= UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(100);
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
        BloodImage.fillAmount = currentHealth / 100;
        //Start shaking image if health is low
    }
}
