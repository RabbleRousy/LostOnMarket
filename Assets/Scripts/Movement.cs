using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private LayerMask jumpable;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private StudioEventEmitter jumpEmitter;
    [SerializeField] private ParticleSystem jumpEffect;

    private float inputDirection;
    private bool isJumping;
    private Rigidbody2D rb;
    private bool isSprinting;

    private void OnMove(InputValue value)
    {
        inputDirection = value.Get<float>();
    }

    private void OnJump(InputValue value)
    {
        isJumping = value.Get<float>() > 0;

        if (IsGrounded() && isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpEmitter.Play();
            jumpEffect.Play();
        }
    }

    private void OnSprint(InputValue value)
    {
        isSprinting = value.Get<float>() > 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        float finalSpeed = isSprinting ? speed * speedMultiplier : speed;
        velocity.x = inputDirection * finalSpeed;

        if (velocity.y < 0)
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (velocity.y > 0 && !isJumping)
        {
            velocity.y += Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 1.2f, jumpable);
    }
}