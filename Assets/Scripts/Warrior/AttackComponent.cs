using UnityEngine;
using PlayerState = StateComponent.PlayerState;

public class AttackComponent : MonoBehaviour
{
    private const int MAX_TARGETS = 20;

    [SerializeField]
    private Collider2D topAttackCollider;

    [SerializeField]
    private Collider2D bottomAttackCollider;

    [SerializeField]
    private int topAttackDamage;

    [SerializeField]
    private int bottomAttackDamage;
    
    [SerializeField]
    private float kickBackHorizontalSpeed;

    [SerializeField]
    private float kickBackVertivalSpeed;

    [SerializeField]
    private AudioSource topAttackAudioSource;

    [SerializeField]
    private AudioSource bottomAttackAudioSource;

    private StateComponent stateComponent;

    private void Start()
    {
        stateComponent = GetComponent<StateComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TopAttack"))
        {
            stateComponent.TryChangeState(PlayerState.TopAttack);
        }

        if (Input.GetButtonDown("BottomAttack"))
        {
            stateComponent.TryChangeState(PlayerState.BottomAttack);
        }
    }

    private void BottomAttack()
    {
        if (bottomAttackAudioSource)
        {
            bottomAttackAudioSource.Play();
        }
        DealDamage(bottomAttackCollider, bottomAttackDamage);
    }

    private void TopAttack()
    {
        if (topAttackAudioSource)
        {
            topAttackAudioSource.Play();
        }
        DealDamage(bottomAttackCollider, bottomAttackDamage);
    }

    private void DealDamage(Collider2D collider, int damage)
    {
        var hitResults = new Collider2D[MAX_TARGETS];
        var count = Physics2D.OverlapCollider(collider, new ContactFilter2D(), hitResults);
        for (int i = 0; i < count; i++)
        {
            var healthComponent = hitResults[i].gameObject.GetComponent<HealthComponent>();
            if (healthComponent)
            {
                healthComponent.DealDamage(damage);
                KickBackEnemies(hitResults, i);
            }
        }
    }

    private void KickBackEnemies(Collider2D[] hitResults, int i)
    {
        var rigidBody = hitResults[i].GetComponent<Rigidbody2D>();
        if (rigidBody)
        {
            var direction = gameObject.transform.localScale.x;
            var kickBackHorizontalForce = Vector2.right * direction * kickBackHorizontalSpeed;
            var kickBackVerticalForce = Vector2.up * direction * kickBackVertivalSpeed;
            rigidBody.AddForce(kickBackHorizontalForce, ForceMode2D.Impulse);
            rigidBody.AddForce(kickBackVerticalForce, ForceMode2D.Impulse);
        }
    }
}
