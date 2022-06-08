using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float attackRate;

    private float nextMeleeAttack;

    private int shootableLayerMask;

    private Animator playerAnimator;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        shootableLayerMask = LayerMask.GetMask("Shootable");
        var root = transform.root;
        playerAnimator = root.GetComponent<Animator>();
        playerController = root.GetComponent<PlayerController>();
        nextMeleeAttack = 0f;
    }

    void FixedUpdate()
    {
        var melee = Input.GetAxis("Fire2");
        if (melee > 0f && nextMeleeAttack < Time.time && !playerController.Running())
        {
            playerAnimator.SetTrigger("melee");
            nextMeleeAttack = Time.time + attackRate;

            var attackedObjects = Physics.OverlapSphere(transform.position, knockBackRadius, shootableLayerMask);
        }
    }
}
