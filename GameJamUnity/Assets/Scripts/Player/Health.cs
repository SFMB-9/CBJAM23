using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Parameters")] [SerializeField]
    private float maxHealth = 100f;

    [SerializeField] private float currentHealth;
    [SerializeField] private float lightHealthRegen = 0.1f;
    [SerializeField] private float darkHealthLoss = 0.1f;
    [SerializeField] private float healthChangeRate = 0.1f;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    public static Action OnDie;

    [Header("Light Range Parameters")] [SerializeField]
    private float lightRange = 5f;

    [SerializeField] private LayerMask lightMask;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Particle System")] [SerializeField]
    private GameObject gainBloodParticles;

    [SerializeField] private GameObject looseBloodParticles;
    
    [SerializeField] private GameObject gameOverCanvas;


    private GameObject targetLight;
    private bool onLight;
    private bool dead;
    private Animator animator;
    PlayerController playerController;

    [SerializeField] private AudioSource damageSoundEffect;

    private void Awake()
    {
        dead = false;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        targetLight = null;
    }

    private void Start()
    {
        StartCoroutine(UpdateHealth());
        StartCoroutine(LightFOVRoutine());
    }


    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }

    private void ApplyHealing(float healing)
    {
        currentHealth += healing;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHeal?.Invoke(currentHealth);
    }

    private void Die()
    {
        StopAllCoroutines();
        damageSoundEffect.clip = null;
        currentHealth = 0;
        dead = true;
        gainBloodParticles.SetActive(false);
        looseBloodParticles.SetActive(false);
        OnDie?.Invoke();

        animator.SetTrigger("Die");
        playerController.LockMovement();
        Debug.Log("Player Died");

        Debug.Log("Game Over");
        gameOverCanvas.SetActive(true);
        Destroy(gameObject);
    }

    private IEnumerator UpdateHealth()
    {
        yield return new WaitForSeconds(5f);
        WaitForSeconds timeToWait = new WaitForSeconds(healthChangeRate);

        while (!dead)
        {
            if (onLight)
            {
                if (targetLight != null)
                    if (targetLight.GetComponent<LightController>().TakeDamage(lightHealthRegen))
                        targetLight = null;
                gainBloodParticles.SetActive(true);
                looseBloodParticles.SetActive(false);
                ApplyHealing(lightHealthRegen);
                damageSoundEffect.Stop();
            }
            else
            {
                ApplyDamage(darkHealthLoss);
                looseBloodParticles.SetActive(true);
                gainBloodParticles.SetActive(false);
                if (!damageSoundEffect.isPlaying)
                {
                    damageSoundEffect.Play();
                }
            }


            yield return timeToWait;
        }
    }


    private IEnumerator LightFOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        float range = lightRange;
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, range, lightMask);

        if (rangeChecks.Length != 0)
        {
            targetLight = rangeChecks[0].gameObject;
            Vector2 directionToTarget = (targetLight.transform.position - transform.position).normalized;

            float distanceToTarget = Vector2.Distance(transform.position, targetLight.transform.position);

            if (distanceToTarget <= range)
            {
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    onLight = true;
                else
                {
                    onLight = false;
                }
            }

        }
        else
        {
            onLight = false;
        }

    }
}
