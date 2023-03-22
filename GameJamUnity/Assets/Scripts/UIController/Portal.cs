using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        LevelLoader.instance.LoadNextLevel();
    }
}
