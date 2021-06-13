using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class Trap : MonoBehaviour
{
    [SerializeField] [TagSelector] string triggerTag = "Player";

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(triggerTag))
        {
            var playerLife = other.GetComponent<PlayerLife>();
            playerLife.Hit();
        }
    }
}
