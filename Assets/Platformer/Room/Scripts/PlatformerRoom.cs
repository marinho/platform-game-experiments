using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class PlatformerRoom : MonoBehaviour
{
    [SerializeField] [TagSelector] string triggerTag = "Player";

    // Collider2D collider2D;
    bool playerIsInside = false;

    // void Awake() {
    //     collider2D = GetComponent<Collider2D>();
    // }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && Camera.current == null)
        {
            playerIsInside = true;
            var camera = Camera.current.GetComponent<PlatformerCamera>();
            camera.MoveToRoom(transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            playerIsInside = false;
        }
    }

}
