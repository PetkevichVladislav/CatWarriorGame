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
    private AudioSource attackAudioSource;

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
            var isChanged = stateComponent.TryChangeState(PlayerState.TopAttack);
            if (isChanged)
            {
                DealDamage(topAttackCollider, topAttackDamage);
            }
        }

        if (Input.GetButtonDown("BottomAttack"))
        {
            var isChanged = stateComponent.TryChangeState(PlayerState.BottomAttack);
            if (isChanged)
            {
                DealDamage(bottomAttackCollider, bottomAttackDamage);
            }
        }
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
            }
        }
    }
}
