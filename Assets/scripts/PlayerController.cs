using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement parameters
    public float runSpeedMultiplier;
    public float walkSpeedMultiplier;

    private bool facingRight = true;

    // jumping parameters
    private bool grounded;
    private Collider[] groundCollisions;
    private float groundColliderRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    new Rigidbody rigidbody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var movementSpeed = Input.GetAxis("Horizontal");
        var sneaking = Input.GetAxisRaw("Fire3");

        animator.SetFloat("sneaking", sneaking);
        animator.SetFloat("movementSpeed", Mathf.Abs(movementSpeed));

        float shooting = Input.GetAxisRaw("Fire1");
        animator.SetFloat("shooting", shooting);

        if ((sneaking > 0f || shooting > 0f) && grounded)
        {
            // walking
            rigidbody.velocity = new Vector3(movementSpeed * walkSpeedMultiplier, rigidbody.velocity.y, 0);
        }
        else
        {
            // running
            rigidbody.velocity = new Vector3(movementSpeed * runSpeedMultiplier, rigidbody.velocity.y, 0);
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
        if (groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        animator.SetBool("grounded", grounded);

        if (movementSpeed > 0 && !facingRight) Flip();
        else if (movementSpeed < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }

    public float GetFacingDirection()
    {
        return facingRight ? 1f : 0f;
    }
}
