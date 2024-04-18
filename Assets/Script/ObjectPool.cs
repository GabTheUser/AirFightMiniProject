using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;

    private List<GameObject> projectilePool;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        projectilePool = new List<GameObject>();

        // Create and deactivate initial pool of projectiles
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    public GameObject GetProjectile(Vector3 startPosition, Quaternion startRotation)
    {

        foreach (GameObject projectile in projectilePool)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.transform.position = startPosition;
                projectile.transform.rotation = startRotation;
                projectile.SetActive(true);
                return projectile;
            }
        }

        // If no inactive projectiles available, instantiate a new one
        GameObject newProjectile = Instantiate(projectilePrefab, startPosition, startRotation);
        projectilePool.Add(newProjectile);
        return newProjectile;
    }
}
