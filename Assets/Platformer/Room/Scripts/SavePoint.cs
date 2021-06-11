using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    void Awake() {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameSaver.Instance.SetSavePointLocation(transform.position);
            GameSaver.Instance.SetSavePointRotation(transform.rotation);
        }
    }

}
