using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        LevelLoader.instance.LoadNextLevel();
        Destroy(gameObject);
    }
}
