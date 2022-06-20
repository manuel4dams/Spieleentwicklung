using MyBox;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [Header("General weapon settings")] //
    public float damage;
    public float fireRate = 0.15f;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public float equipDelay;
    public AudioClip equipSound;
    public bool ammunitionEnabled = true;
    [ConditionalField(nameof(ammunitionEnabled))] //
    public int startRounds = 100;
    [ConditionalField(nameof(ammunitionEnabled))] //
    public int maxRounds = 100;

    // TODO
    // shootDelay
    // animations?

    [Header("General UI settings")] //
    public Image weaponImage;

    [Header("References")] //
    public Transform weaponOrigin;
    public Sprite weaponSprite;
    public Slider ammunitionSlider;
    [Tooltip("The source where the sound is coming from when using this weapon")] //
    public AudioSource weaponAudioSource;

    // Variables
    private float timeNextBulletAllowed;
    [SerializeField] [ReadOnly] [ConditionalField(nameof(ammunitionEnabled))] //
    private int remainingRounds;

    void Start()
    {
        remainingRounds = startRounds;

        if (weaponImage)
            weaponImage.sprite = weaponSprite;
    }

    void Update()
    {
        if (ammunitionEnabled)
        {
            ammunitionSlider.maxValue = maxRounds;
            ammunitionSlider.value = remainingRounds;
        }
    }

    public void Equip()
    {
        timeNextBulletAllowed = Time.realtimeSinceStartup + equipDelay;

        // Play equip sound
        if (equipSound)
        {
            weaponAudioSource.clip = equipSound;
            weaponAudioSource.Play();
        }
    }

    public void Fire(Vector3 direction)
    {
        // Check if its allowed to fire already
        if (Time.realtimeSinceStartup < timeNextBulletAllowed)
            return;

        timeNextBulletAllowed = Time.realtimeSinceStartup + fireRate;

        // Play shoot sound
        if (shootSound)
        {
            weaponAudioSource.clip = shootSound;
            weaponAudioSource.Play();
        }

        FireImplementation(weaponOrigin, direction);

        remainingRounds--;
    }

    protected abstract void FireImplementation(Transform weaponOrigin, Vector3 direction);

    /// <returns>False if at max ammunition</returns>
    public bool RestockAmmunition(int ammunition)
    {
        // If we cannot pick rounds up, skip and notify
        if (remainingRounds >= maxRounds)
            return false;

        remainingRounds += ammunition;

        // Cap the rounds to the max
        if (remainingRounds > maxRounds)
            remainingRounds = maxRounds;

        // Play reload sound
        if (reloadSound)
        {
            weaponAudioSource.clip = reloadSound;
            weaponAudioSource.Play();
        }

        // Return that rounds where picked up
        return true;
    }
}
