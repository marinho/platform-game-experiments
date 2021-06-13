using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class SavePoint : MonoBehaviour
{
    [SerializeField] [TagSelector] string triggerTag = "Player";

    void Awake() {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            GameSaver.Instance.SetSavePointLocation(transform.position);
            GameSaver.Instance.SetSavePointRotation(transform.rotation);
        }
    }

}
