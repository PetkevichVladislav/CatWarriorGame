using UnityEngine;

public class EnemieDeathComponent : MonoBehaviour
{
    void Start()
    {
        GetComponent<HealthComponent>().OnDeath += OnDeath;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
