using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieController : MonoBehaviour
{
    // TODO
    // Movement behavior is strange zombie should walk and only change to run
    // when player enters trigger and certain time has passed, on trigger exit should walk again

    public GameObject modelToFlip;
    public GameObject ragDollDead;
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

    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

        running = false;
        alerted = false;
        firstDetection = false;

        movementSpeed = walkSpeed;
        if (Random.Range(0, 10) > 5)
            Flip();
    }

    void FixedUpdate()
    {
        // Stop if player got destroyed
        if (!detectedPlayer)
            alerted = false;

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
        if (alerted && !facingRight)
            velocity = new Vector3(movementSpeed * -1, velocity.y, 0);
        else if (alerted && facingRight)
            velocity = new Vector3(movementSpeed, velocity.y, 0);
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
        if (running) return;
        if (Random.Range(0, 10) > 5 && nextIdleSound < Time.time)
        {
            audioSource.clip = idleSounds[Random.Range(0, idleSounds.Length)];
            audioSource.Play();
            nextIdleSound = idleSoundTime + Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || alerted) return;
        alerted = true;
        detectedPlayer = other.transform;
        animator.SetBool("alerted", alerted);
        if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
        else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
    }

    void OnTriggerExit(Collider other)
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
        var transform1 = modelToFlip.transform;
        var scale = transform1.localScale;
        scale.z *= -1f;
        modelToFlip.transform.localScale = scale;
    }

    public void RagDollDeath()
    {
        var root = transform.root;
        var ragDoll = Instantiate(ragDollDead, root.transform.position, Quaternion.identity) as GameObject;
        var ragDollMaster = ragDoll.transform.Find("master");
        var zombieMaster = root.Find("master");

        // workaround for zombie rotation we scale -1 instead of actually rotate the charackter 
        var wasFacingRight = true;
        if (!facingRight)
        {
            wasFacingRight = false;
            Flip();
        }

        var ragDollJoints = ragDollMaster.GetComponentsInChildren<Transform>();
        var currentJoints = zombieMaster.GetComponentsInChildren<Transform>();
        
        for (var i = 0; i < ragDollJoints.Length; i++)
        {
            for (var j = 0; j < currentJoints.Length; j++)
            {
                if (currentJoints[j].name == ragDollJoints[i].name)
                {
                    ragDollJoints[i].position = currentJoints[j].position;
                    ragDollJoints[i].rotation = currentJoints[j].rotation;
                    break;
                }
            }
        }
        
        if (wasFacingRight)
        {
            var rotation = new Vector3(0, 0, 0);
            ragDoll.transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            var rotation = new Vector3(0, 90, 0);
            ragDoll.transform.rotation = Quaternion.Euler(rotation);
        }
        
        var zombieMesh = transform.root.transform.Find("Zombie Soldier");
        var ragDollMesh = ragDoll.transform.Find("Zombie Soldier");
        
        ragDollMesh.GetComponent<Renderer>().material = zombieMesh.GetComponent<Renderer>().material;
    }
}
