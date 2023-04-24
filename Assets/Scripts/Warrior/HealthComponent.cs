using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HealthComponent : MonoBehaviour
{
    /// <summary>
    /// Delay to change color when person were damaged.
    /// </summary>
    public float changeColorDelay;

    /// <summary>
    /// Helthbar
    /// </summary>
    public Slider healthbar;

    /// <summary>
    /// Max number of health points.
    /// </summary>
    public int maxHealth = 100;

    /// <summary>
    /// The color of person if it is damaged.
    /// </summary>
    public Color damageColor;

    public AudioSource damageAudioSource;

    /// <summary>
    /// The person number of lives.
    /// </summary>
    public int lives;

    /// <summary>
    /// The current number of health.
    /// </summary>
    private int health;

    private SpriteRenderer spriteRendered;

    public event Action OnDeath;

    private void Start()
    {
        health = maxHealth;
        spriteRendered = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Heal person.
    /// </summary>
    /// <param name="healPoints">The amount of health points the person will be healed.</param>
    public void Heal(int healPoints)
    {
        if (health + healPoints > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healPoints;
        }
    }

    /// <summary>
    /// Damage person.
    /// </summary>
    /// <param name="damage">The amount of damage points the person will be dealt.</param>
    public void DealDamage(int damage)
    {
        if (health - damage <= 0)
        {
            Kill();
        }
        else
        {
            if (damageAudioSource)
            {
                damageAudioSource.Play();
            } 
            StartCoroutine(ChangeColor());
            health -= damage;
        }
        UpdateHelthbar();
    }

    private IEnumerator ChangeColor()
    {
        spriteRendered.color = damageColor;
        yield return new WaitForSeconds(changeColorDelay);
        spriteRendered.color = Color.white;
    }

    /// <summary>
    /// Decrease amount of lives and change scene if there are no lives.
    /// </summary>
    private void Kill()
    {
        lives--;
        Debug.Log(lives);
        if (lives <= 0)
        {
            OnDeath?.Invoke();
        }
        health = maxHealth;
    }

    private void UpdateHelthbar()
    {
        if (healthbar)
        {
            healthbar.value = (float)health / maxHealth;
        }
    }
}
