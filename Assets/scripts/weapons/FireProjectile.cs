using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour
{
    public float fireRate = 0.15f;
    public GameObject projectile;
    public string keyBind = "Fire1";
    public AudioSource gunMuzzleAudioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Slider ammunitionSlider;
    public int maxRounds = 100;
    public int startRounds = 100;
    private int remainingRounds;

    private float nextBulletAllowed;

    public Sprite weaponIcon;
    public Image weaponImage;

    // Start is called before the first frame update
    void Start()
    {
        nextBulletAllowed = 0f;
        remainingRounds = startRounds;

        ammunitionSlider.maxValue = maxRounds;
        ammunitionSlider.value = remainingRounds;

    }

    public void InitWeapon()
    {
        gunMuzzleAudioSource.clip = reloadSound;
        gunMuzzleAudioSource.Play();
        nextBulletAllowed = 0f;
        ammunitionSlider.maxValue = maxRounds;
        ammunitionSlider.value = remainingRounds;
        weaponImage.sprite = weaponIcon;
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = transform.root.GetComponent<PlayerController>();
        if (!(Input.GetAxisRaw(keyBind) > 0f) || !(nextBulletAllowed < Time.time) || remainingRounds <= 0) return;
        nextBulletAllowed = Time.time + fireRate;
        var rotation = playerController.GetFacingDirection() == 0f ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);

        Instantiate(projectile, transform.position, Quaternion.Euler(rotation));

        gunMuzzleAudioSource.clip = shootSound;
        gunMuzzleAudioSource.Play();

        remainingRounds--;
        ammunitionSlider.value = remainingRounds;
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
        gunMuzzleAudioSource.clip = reloadSound;
        gunMuzzleAudioSource.Play();
        ammunitionSlider.value = remainingRounds;
        return true;
    }
}
