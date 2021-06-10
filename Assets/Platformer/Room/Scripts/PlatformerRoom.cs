using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerRoom : MonoBehaviour
{

    // Collider2D collider2D;
    bool playerIsInside = false;

    // void Awake() {
    //     collider2D = GetComponent<Collider2D>();
    // }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            var camera = PlatformerCamera.Instance;
            camera.MoveToRoom(transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }

}
