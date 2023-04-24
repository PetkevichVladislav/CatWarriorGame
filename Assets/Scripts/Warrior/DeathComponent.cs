using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    private const int DeathSceneIndex = 2;

    void Start()
    {
        GetComponent<HealthComponent>().OnDeath += OnDeath;
    }

    public void OnDeath()
    {
        SceneManager.LoadScene(DeathSceneIndex, LoadSceneMode.Single);
    }
}
