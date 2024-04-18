using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunControl : MonoBehaviour
{
    public Transform[] firePoints;
    public ObjectPool projectilePool;
    public float fireRate = 2f;
    private float fireTimer = 0f;

    private bool isShooting = false;
    [SerializeField] private GameObject[] vCams;
    [SerializeField] private ParticleSystem[] muzzleFires;
    [SerializeField] private AudioSource[] muzzleSources;

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }

        // Check if the left mouse button is released
        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            fireTimer = 0f; // Reset the fire timer when releasing the button
        }

        // If shooting, increment the fire timer
        if (isShooting)
        {
            fireTimer += Time.deltaTime;

            // Check if it's time to shoot based on the fire rate
            if (fireTimer >= 1f / fireRate)
            {
                Shoot();
                fireTimer = 0f; // Reset the fire timer
            }
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            // Calculate the direction of the bullet based on the fire point's forward vector
            Vector3 bulletDirection = firePoints[i].forward;
            if (muzzleSources[i] != null)
            {
                muzzleSources[i].Play();
            }
            muzzleFires[i].gameObject.SetActive(true);
            // Get a projectile from the object pool and spawn it at the fire point with the correct direction
            GameObject bullet = projectilePool.GetProjectile(firePoints[i].position, Quaternion.LookRotation(bulletDirection));

            // Set the direction of the bullet
            bullet.GetComponent<BulletController>().SetDirection(bulletDirection);
        }
    }
}

