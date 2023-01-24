using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private NPCController npcController;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        npcController = GetComponent<NPCController>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void MoveTo(Vector3 destination, float speedFraction = 2f)
    {

        animator.SetTrigger("Run");
        if (destination.x < transform.position.x)
        {
            if (npcController.GetIsInfected())
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            if (npcController.GetIsInfected())
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        agent.SetDestination(destination);
        agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        agent.isStopped = false;
        StartCoroutine(DestinationCheck());
    }

    public void Cancel()
    {
        animator.SetTrigger("Stop");
        agent.isStopped = true;
    }

    public bool HasPath()
    {
        return agent.hasPath;
    }

    public void StopTheCoroutine()
    {
        StopAllCoroutines();
    }

    private IEnumerator DestinationCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        animator.SetTrigger("Stop");
                        yield break;
                    }
                }
            }
            
        }
    }
}
