using UnityEngine;
using System.Linq;

public class StateComponent : MonoBehaviour
{
    private Animator animator;

    private PlayerState _playerState;

    private readonly PlayerState[] _attacksState = new[] { PlayerState.TopAttack, PlayerState.BottomAttack };

    private PlayerState playerState
    {
        get
        {
            return _playerState;
        }

        set
        {
            // if states didn't changed - return
            if (_playerState == value)
            {
                return;
            }

            _playerState = value;
            // Set state changes in animation
            animator.SetInteger("PlayerState", (int)_playerState);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool TryChangeState(PlayerState newState)
    {
        if (_attacksState.Contains(playerState))
        {
            return false;
        }

        playerState = newState;
        return true;
    }

    public void OnAnimationEnded()
    {
        playerState = PlayerState.Idle;
    }

    public PlayerState GetState()
    {
        return playerState;
    }

    public enum PlayerState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        TopAttack = 3,
        BottomAttack = 4,
    }
}
