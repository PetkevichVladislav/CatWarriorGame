using UnityEngine;

public class StateComponent : MonoBehaviour
{
    private Animator animator;

    private PlayerState _playerState;

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

    public void TryChangeState(PlayerState newState)
    {
        playerState = newState;
    }

    public PlayerState GetState()
    {
        return _playerState;
    }

    public enum PlayerState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
    }
}
