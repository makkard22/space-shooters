using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireRate = 0.5f;

    [Header("Audio")]
    public AudioClip laserSound;
    [Range(0f, 1f)]
    public float laserVolume = 0.7f;

    private float nextFireTime = 0f;
    private AudioSource audioSource;

    void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (laserPrefab != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 0.5f, -1);
            GameObject laser = Instantiate(laserPrefab, spawnPos, transform.rotation);

            // Play laser sound
            PlayLaserSound();
        }
    }

    void PlayLaserSound()
    {
        if (laserSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(laserSound, laserVolume);
        }
    }
}