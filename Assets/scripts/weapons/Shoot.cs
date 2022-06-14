using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public float fireRate = 0.15f;
    public GameObject projectile;
    public string keyBind = "Fire1";
    private AudioSource GunMuzzleAudioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Slider AmmunitionSlider;
    public int maxRounds = 100;
    public int startRounds = 100;
    private int remainingRounds;

    private float nextBulletAllowed;

    // Start is called before the first frame update
    void Start()
    {
        nextBulletAllowed = 0f;
        remainingRounds = startRounds;

        AmmunitionSlider.maxValue = maxRounds;
        AmmunitionSlider.value = remainingRounds;

        GunMuzzleAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = transform.root.GetComponent<PlayerController>();
        if (Input.GetAxisRaw(keyBind) > 0f && nextBulletAllowed < Time.time && remainingRounds > 0)
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

            GunMuzzleAudioSource.clip = shootSound;
            GunMuzzleAudioSource.Play();

            remainingRounds--;
            AmmunitionSlider.value = remainingRounds;
        }
    }

    // return false if player can't be store more ammunition
    public bool RestockAmmunition(int ammunition)
    {
        // already max Ammunition or above 
        if (remainingRounds >= maxRounds)
        {
            return false;
        }
        remainingRounds += ammunition;
        if (remainingRounds > maxRounds)
        {
            remainingRounds = maxRounds;
        }
        GunMuzzleAudioSource.clip = reloadSound;
        GunMuzzleAudioSource.Play();
        AmmunitionSlider.value = remainingRounds;
        return true;
    }
}
