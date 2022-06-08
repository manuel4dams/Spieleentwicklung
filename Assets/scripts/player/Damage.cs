using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    private float nextDamageAllowed;
    private bool playerInRange;

    private GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        nextDamageAllowed = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            Attack();
        }
    }

    // TODO may be obsolete workaround
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // TODO may be obsolete workaround
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Attack()
    {
        if (!(nextDamageAllowed <= Time.time)) return;
        playerHealth.DamagePlayer(damage);
        nextDamageAllowed = Time.time + damageRate;

        PushBack(player.transform);
    }

    private void PushBack(Transform pushedObject)
    {
        var pushDirection = new Vector3(0, (pushedObject.position.y - transform.position.y), 0);
        pushDirection *= pushBackForce;

        var pushedObjectRigidBody = pushedObject.GetComponent<Rigidbody>();
        pushedObjectRigidBody.velocity = Vector3.zero;
        pushedObjectRigidBody.AddForce(pushDirection, ForceMode.Impulse);
    }
}
