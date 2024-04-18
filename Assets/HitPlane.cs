using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlane : MonoBehaviour
{
    private float timer = 5;
    [SerializeField] private MainMenuControl mainMenuControl;
    [SerializeField] private GameObject explosionEffect;

    private bool hitten;

    private void Update()
    {
        if (hitten)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                mainMenuControl.EndScreenStats(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            if (!hitten)
            {
                Instantiate(explosionEffect, collision.collider.gameObject.transform.position, collision.collider.gameObject.transform.rotation);
                Instantiate(explosionEffect, transform.position, transform.rotation);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                hitten = true;
            }
        }
    }
}
