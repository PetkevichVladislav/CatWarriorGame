using UnityEngine;
using PlayerState = StateComponent.PlayerState;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private AudioSource jumpAudioSource;

    private StateComponent stateComponent;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stateComponent = GetComponent<StateComponent>();
    }

    private void Update()
    {
        var dirY = rigidBody.velocity.y;
        var dirX = Input.GetAxis("Horizontal");

        // if key in axis pressed, multiply on move speed x coord, y coord doesn't change
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);

        // if player pressed key "jump" and them in not state "fly" add force to jump.
        if (Input.GetButtonDown("Jump") && stateComponent.GetState() != PlayerState.Jump)
        {
            if (jumpAudioSource)
            {
                jumpAudioSource.Play();
            }
            
            rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

        // update player state
        UpdateState(dirX, dirY);
    }

    private void UpdateState(float dirX, float dirY)
    {
        // flip sprite direction
        if (dirX != 0)
        {
            var localScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(dirX < 0 ? -1 : 1, localScale.y, localScale.z);
        }

        //Important to update horizontal state before vertical
        // if player is moving - change them state to run otherwise to idle
        if (Mathf.Abs(dirX) > 0.01f)
        {
            stateComponent.TryChangeState(PlayerState.Run);
        }
        else
        {
            stateComponent.TryChangeState(PlayerState.Idle);
        }

        // if player is falling - change them state to jump
        if (Mathf.Abs(dirY) > 0.01f)
        {
            stateComponent.TryChangeState(PlayerState.Jump);
        }
        // if player landed - change them state to idle
        else if (Mathf.Abs(dirY) <= 0.01f && stateComponent.GetState() == PlayerState.Jump)
        {
            stateComponent.TryChangeState(PlayerState.Idle);
        }
    }
}
