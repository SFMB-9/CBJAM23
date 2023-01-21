using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float Health {set; get;}	
    public bool Tergetable {set; get;}

    public float _health = 10f;
    public bool _targetable = true;

    public void OnHit(float damage, Vector2 knockback)
    {
        
    }

    public void OnHit(float damage)
    {
        
    }
    public void OnObjectDestroyed() {
    }

}
