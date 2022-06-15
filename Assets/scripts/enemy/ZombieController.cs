using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieController : MonoBehaviour
{
    public GameObject gameObject;
    public AudioClip[] idleSounds;
    public float idleSoundTime;
    private AudioSource audioSource;
    private float nextIdleSound = 0f;
    public float detectionTime;
    private float startRun;
    private bool firstDetection;
    private bool alerted;
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;
    private float movementSpeed;
    private bool running;
    private Rigidbody rigidbody;
    private Animator animator;
    private Transform detectedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

        running = false;
        alerted = false;
        firstDetection = false;

        movementSpeed = walkSpeed;
        if (Random.Range(0, 10) > 5) Flip();
    }

    void FixedUpdate()
    {
        if (alerted)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }

        var velocity = rigidbody.velocity;
        velocity = alerted switch
        {
            true when !facingRight => new Vector3(movementSpeed * -1, velocity.y, 0),
            true when facingRight => new Vector3(movementSpeed, velocity.y, 0),
            _ => velocity
        };
        rigidbody.velocity = velocity;

        if (!running && alerted)
        {
            if (startRun < Time.time)
            {
                movementSpeed = runSpeed;
                animator.SetTrigger("run");
                running = true;
            }
        }
        if (!running)
        {
            if (Random.Range(0, 10) > 5 && nextIdleSound < Time.time)
            {
                audioSource.clip = idleSounds[Random.Range(0, idleSounds.Length)];
                audioSource.Play();
                nextIdleSound = idleSoundTime + Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || alerted) return;
        alerted = true;
        detectedPlayer = other.transform;
        animator.SetBool("alerted", alerted);
        if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
        else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        firstDetection = false;
        if (!running) return;
        animator.SetTrigger("run");
        movementSpeed = walkSpeed;
        running = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var transform1 = gameObject.transform;
        var scale = transform1.localScale;
        scale.z *= -1;
        gameObject.transform.localScale = scale;
    }
}
