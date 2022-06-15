using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement parameters
    public float runSpeedMultiplier;
    public float walkSpeedMultiplier;
    private bool running;

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
        running = false;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
            running = false;
        }
        // running
        else
        {
            rigidbody.velocity = new Vector3(movementSpeed * runSpeedMultiplier, rigidbody.velocity.y, 0);
            running = Mathf.Abs(movementSpeed) != 0f;
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
        var transform1 = transform;
        var scale = transform1.localScale;
        scale.z *= -1;
        transform1.localScale = scale;
    }

    public float GetFacingDirection()
    {
        return facingRight ? 1f : 0f;
    }

    public bool Running()
    {
        return running;
    }
}
