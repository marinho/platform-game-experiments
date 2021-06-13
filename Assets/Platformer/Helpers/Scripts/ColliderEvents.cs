using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSWhitehouse.TagSelector;

public class ColliderEvents : MonoBehaviour
{
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onExit;
    [SerializeField] [TagSelector] List<string> triggerTags = new List<string>{ "Player"};

    void OnTriggerEnter2D(Collider2D other) {
        if (triggerTags.Count == 0 || triggerTags.Contains(other.tag))
        {
            onEnter.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (triggerTags.Count == 0 || triggerTags.Contains(other.tag))
        {
            onExit.Invoke();
        }
    }
}
