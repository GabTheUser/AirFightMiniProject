using System.Runtime.InteropServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Speed of the bullet
    public float bulletSpeed = 10f;

    // Lifetime of the bullet
    public float bulletLifetime = 2f;

    private float currentBulletLifeTime;
    // Damage inflicted by the bullet
    public int bulletDamage = 1;
    public Cinemachine.CinemachineImpulseSource impulse;
    public GameObject effect;

    private Vector3 bulletDirection; // Direction of the bullet

    private void OnEnable()
    {
        impulse.GenerateImpulse(Camera.main.transform.forward);
    }

    // Set the direction of the bullet
    public void SetDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }

    void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime, Space.World);

        // Update the bullet's lifetime
        currentBulletLifeTime += Time.deltaTime;
        if (currentBulletLifeTime > bulletLifetime)
        {
            currentBulletLifeTime = 0;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Instantiate(effect, transform.position, transform.rotation);
            other.GetComponentInParent<HealthManager>().TakeDamage(bulletDamage);
            gameObject.SetActive(false);
        }
    }
}
