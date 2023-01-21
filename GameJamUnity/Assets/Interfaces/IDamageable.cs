using UnityEngine;

// Instanciar en funcion OnTriggerEnter/OnCollisionEnter de hitbox para luces
public interface IDamageable {
    public float Health {set; get;}
    public void OnHit(float damage, Vector2 knockback); // !!! Agregar OnHit -> rigidbody.AddForce(knockback) a cualquier npc/objeto que tome daño
    public void OnHit(float damage); // Simplemente aplicar daño sin knockback
}