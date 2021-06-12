using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            var playerLife = other.GetComponent<PlayerLife>();
            playerLife.Hit();
        }
    }
}
