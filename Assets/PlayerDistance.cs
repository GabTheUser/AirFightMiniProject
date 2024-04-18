using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance;
    [SerializeField] private MainMenuControl mainMenuControl;
    private void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > distance)
        {
            mainMenuControl.EndScreenStats(false);
            Debug.Log("lost");
        }
    }
}
