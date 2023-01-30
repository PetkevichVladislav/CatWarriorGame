using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private AudioSource jumpAudioSource;

    private Rigidbody2D rigidBody;

    private SpriteRenderer spriteRendered;

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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRendered = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var dirY = rigidBody.velocity.y;
        var dirX = Input.GetAxis("Horizontal");
        // if key in axis pressed, multiply on move speed x coord, y coord doesn't change
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);

        // if player pressed key "jump" and them in not state "fly" add force to jump.
        if (Input.GetButtonDown("Jump") && playerState != PlayerState.Jump)
        {
            jumpAudioSource.Play();
            rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

        // update player state
        UpdateState(dirX, dirY);
    }

    private void UpdateState(float dirX, float dirY)
    {
        // flip sprite direction
        spriteRendered.flipX = dirX < 0;

        //Important to update horizontal state before vertical
        // if player is moving - change them state to run otherwise to idle
        if (Mathf.Abs(dirX) > 0.01f)
        {
            playerState = PlayerState.Run;
        }
        else
        {
            playerState = PlayerState.Idle;
        }

        // if player is falling - change them state to jump
        if (Mathf.Abs(dirY) > 0.01f)
        {
            playerState = PlayerState.Jump;
        }
        // if player landed - change them state to idle
        else if (Mathf.Abs(dirY) <= 0.01f && playerState == PlayerState.Jump)
        {
            playerState = PlayerState.Idle;
        }
    }

    private enum PlayerState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
    }
}
