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
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Light2D light;
    private Collider2D lightCollider;

    private void Awake()
    {
        lightCollider = GetComponent<Collider2D>();
        light = GetComponent<Light2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = lightOn;
    }


    public bool TakeDamage(float damage)
    {
        lightHealth -= damage;
        light.intensity = lightHealth / 100f;
        if (lightHealth <= 0)
        {
            TurnOff();
            return true;
        }
        return false;
    }
    
    private void TurnOff()
    {
        spriteRenderer.sprite = lightOff;
        light.enabled = false;
        lightCollider.enabled = false;
    }
    
}
