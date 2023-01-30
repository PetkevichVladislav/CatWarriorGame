using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthComponent = collision.gameObject.GetComponent<HealthComponent>();
        if (healthComponent)
        {
            healthComponent.DealDamage(damage);
        }
    }
}
