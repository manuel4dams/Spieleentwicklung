using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireRate = 0.15f;
    public GameObject projectile;
    public string keyBind = "Fire1";

    private float nextBulletAllowed;

    // Start is called before the first frame update
    void Start()
    {
        nextBulletAllowed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = transform.root.GetComponent<PlayerController>();
        if (Input.GetAxisRaw(keyBind) > 0f && nextBulletAllowed < Time.time)
        {
            nextBulletAllowed = Time.time + fireRate;
            Vector3 rotation;
            if (playerController.GetFacingDirection() == 0f)
            {
                rotation = new Vector3(0, -90, 0);
            }
            else
            {
                rotation = new Vector3(0, 90, 0);
            }

            Instantiate(projectile, transform.position, Quaternion.Euler(rotation));
        }
    }
}
