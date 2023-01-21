using UnityEngine;

public interface IHealable {
    public float Health {set; get;}
    public void OnHit(float health, Vector2 knockback);
}