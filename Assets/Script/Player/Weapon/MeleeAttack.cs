using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float knockBackRadius;
    public float attackRate;
    public float damage;

    private float nextMeleeAttack;

    private int shootableLayerMask;

    private Animator playerAnimator;

    private PlayerMovementController playerMovementController;

    // Start is called before the first frame update
    void Start()
    {
        shootableLayerMask = LayerMask.GetMask("Shootable");
        var root = transform.root;
        playerAnimator = root.GetComponent<Animator>();
        playerMovementController = root.GetComponent<PlayerMovementController>();
        nextMeleeAttack = 0f;
    }

    void FixedUpdate()
    {
        var melee = Input.GetAxis("Fire2");
        if (melee > 0f && nextMeleeAttack < Time.time && !playerMovementController.Running)
        {
            playerAnimator.SetTrigger("melee");
            nextMeleeAttack = Time.time + attackRate;

            var attackedObjects = Physics.OverlapSphere(transform.position, knockBackRadius, shootableLayerMask);
            foreach (var collider in attackedObjects)
            {
                if (collider.CompareTag("Enemy"))
                {
                    var enemyHealth = collider.GetComponent<EnemyHealth>();
                    enemyHealth.DamageEnemy(damage);
                }
            }
        }
    }
}