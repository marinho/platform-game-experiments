using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class SavePoint : MonoBehaviour
{
    [SerializeField] [TagSelector] string triggerTag = "Player";
    [SerializeField] bool storePosition = true;

    void Awake() {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && storePosition)
        {
            var chapter = Chapter.GetForScene(gameObject.scene);
            chapter.SetLatestLocation(transform);
            // GameSaver.Instance.SetSavePointLocation(transform.position);
            // GameSaver.Instance.SetSavePointRotation(transform.rotation);
        }
    }

}
