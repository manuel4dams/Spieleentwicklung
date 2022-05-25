using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // movement parameters
    public float runSpeedMultiplier;

    new Rigidbody rigidbody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        var movementSpeed = Input.GetAxis("Horizontal");

        CheckRotation(movementSpeed);
        animator.SetFloat("movementSpeed", Mathf.Abs(movementSpeed));
        rigidbody.velocity = new Vector3(movementSpeed * runSpeedMultiplier, rigidbody.velocity.y, 0);
    }

    void CheckRotation(float movementSpeed)
    {
        var scale = transform.localScale;
        scale.z = movementSpeed switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => scale.z
        };
        transform.localScale = scale;
    }
}
