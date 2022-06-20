using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement parameters")] //
    public float runSpeedMultiplier;
    public float walkSpeedMultiplier;

    [Header("Jump parameters")] //
    public LayerMask groundLayer;
    public float jumpHeight;
    public float groundColliderRadius = 0.2f;

    [Header("References")] //
    public Transform playerTransform;
    public Transform groundCheck;
    public new Rigidbody rigidbody;
    public Animator animator;

    // Variables
    private bool facingRight = true;
    private bool grounded;
    private Collider[] groundCollisions;

    // Properties
    public float FacingDirection => facingRight ? 1f : 0f;
    public bool Running
    {
        get;
        private set;
    }

    void FixedUpdate()
    {
        var movementSpeed = Input.GetAxis("Horizontal");
        var sneaking = Input.GetAxisRaw("Fire3");

        animator.SetFloat("sneaking", sneaking);
        animator.SetFloat("movementSpeed", Mathf.Abs(movementSpeed));

        var shooting = Input.GetAxisRaw("Fire1");
        animator.SetFloat("shooting", shooting);

        // walking
        if ((sneaking > 0f || shooting > 0f) && grounded)
        {
            rigidbody.velocity = new Vector3(movementSpeed * walkSpeedMultiplier, rigidbody.velocity.y, 0);
            Running = false;
        }
        // running
        else
        {
            rigidbody.velocity = new Vector3(movementSpeed * runSpeedMultiplier, rigidbody.velocity.y, 0);
            Running = Mathf.Abs(movementSpeed) != 0f;
        }

        // handle jump
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            animator.SetBool("grounded", grounded);
            rigidbody.AddForce(new Vector3(0, jumpHeight, 0));
        }

        // create ground collider
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundColliderRadius, groundLayer);
        // check ground collision
        grounded = groundCollisions.Length > 0;
        animator.SetBool("grounded", grounded);

        switch (movementSpeed)
        {
            case > 0 when !facingRight:
            case < 0 when facingRight:
                Flip();
                break;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scale = playerTransform.localScale;
        scale.z *= -1;
        playerTransform.localScale = scale;
    }
}
