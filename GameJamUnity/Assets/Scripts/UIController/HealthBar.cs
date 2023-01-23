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
        if (currentHealth < 20)
        {
            StartCoroutine(ShakeImage());
        }
    }

    private IEnumerator ShakeImage()
    {
        float shakeTime = 0.5f;
        float shakeAmount = 0.1f;
        float shakeSpeed = 10f;
        float shakeTimer = 0f;
        Vector3 originalPos = BloodImage.transform.localPosition;
        while (shakeTimer < shakeTime)
        {
            Vector3 newPos = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            BloodImage.transform.localPosition = Vector3.Lerp(BloodImage.transform.localPosition, newPos, shakeSpeed * Time.deltaTime);
            shakeTimer += Time.deltaTime;
            yield return null;
        }
        BloodImage.transform.localPosition = originalPos;
    }
}
