using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public GameObject smokeEffect, explosionEffect;
    private int health;
    [SerializeField] private AiPatrol aiPatrol;
    private bool hitten;
    private float timer;
    [SerializeField] private MainMenuControl mainMenuControl;
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 )
            {
                mainMenuControl.EndScreenStats(true);
                gameObject.SetActive(false);
                Debug.Log("won");
            }
        }
    }
    public void TakeDamage(int currentDamage)
    {
        if (health > 0)
        {
            health -= currentDamage;
            if (health <= 0)
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Animator>().enabled = false;
                timer = 6f;
                //enabled = false;
            }
            if (health <= maxHealth / 2 && !hitten)
            {
                smokeEffect.SetActive(true);
                aiPatrol.speed *= 0.7f;
                hitten = true;
            }
        }
    }
}
