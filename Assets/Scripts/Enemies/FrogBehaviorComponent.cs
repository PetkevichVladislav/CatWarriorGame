using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehaviorComponent : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    private SpriteRenderer spriteRendered;

    private Animator animator;

    [SerializeField]
    private float JumpDelay;

    [SerializeField]
    private float jumpHorizontalSpeed;

    [SerializeField]
    private float jumpVerticalSpeed;

    [SerializeField]
    private AudioSource jumpAudioSource;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRendered = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // Run timer;
        StartCoroutine(JumpSchedule());
    }

    private IEnumerator JumpSchedule()
    {
        while (true)
        {
            Jump();
            yield return new WaitForSeconds(JumpDelay);
        }
    }

    private void Jump()
    {
        jumpAudioSource.Play();
        var isLeft = Random.Range(0, 2) == 1;
        var jumpVectorVertical = Vector2.up * jumpVerticalSpeed;
        var jumpVectorHorizontal = (isLeft ? Vector2.left : Vector2.right) * jumpHorizontalSpeed;
        // flip sprite direction
        spriteRendered.flipX = isLeft;
        // Trigger animation
        animator.SetTrigger("FrogJumpTrigger");

        rigidBody.AddForce(jumpVectorHorizontal + jumpVectorVertical, ForceMode2D.Impulse);
    }
}
