using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class PlatformerRoom : MonoBehaviour
{
    [SerializeField] [TagSelector] string triggerTag = "Player";

    bool playerIsInside = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            playerIsInside = true;
            PlayerMovement.Instance.SetCurrentRoom(this);
            
            if (Camera.current != null) {
                var camera = Camera.current.GetComponent<PlatformerCamera>();
                camera.MoveToRoom(transform.position);
            }
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
