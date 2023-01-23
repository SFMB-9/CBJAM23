using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private float lightHealth = 100f;
    [SerializeField] private Sprite lightOn;
    [SerializeField] private Sprite lightOff;
    [SerializeField] private Light2D light;
    [SerializeField] private Collider2D lightCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = lightOn;
    }


    public void TakeDamage(float damage)
    {
        lightHealth -= damage;
        light.intensity = lightHealth / 100f;
        if (lightHealth <= 0)
        {
            TurnOff();
        }
    }
    
    private void TurnOff()
    {
        spriteRenderer.sprite = lightOff;
        light.enabled = false;
        lightCollider.enabled = false;
    }
    
}
