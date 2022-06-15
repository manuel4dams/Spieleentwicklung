using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;
    public float speed;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        // player facing right
        if (transform.rotation.y > 0f)
        {
            rigidbody.AddForce(Vector3.right * speed, ForceMode.Impulse);
        }
        // player facing left
        else
        {
            rigidbody.AddForce(Vector3.right * -speed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Shootable")) return;
        switch (other.gameObject.tag)
        {
            case "Enemy":
                var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.DamageEnemy(damage);
                enemyHealth.AddFire();
                break;
            case "Crate":
                other.gameObject.GetComponent<Crate>().DestroyCrate();
                break;
            case "Barrel":
                other.gameObject.GetComponent<Barrel>().HitBarrel();
                break;
        }
        rigidbody.velocity = Vector3.zero;
        Destroy(gameObject);
    }
}
