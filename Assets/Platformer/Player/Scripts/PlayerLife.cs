using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] UnityEvent onDie;
    [SerializeField] UnityEvent onHit;

    void Start() {
        Respawn();
    }

    public void Respawn() {
        var chapter = Chapter.GetForScene(gameObject.scene);
        var latestLocation = chapter.GetLatestLocation();
        if (latestLocation != null) {
            transform.position = latestLocation.position;
            transform.rotation = latestLocation.rotation;
        }
        // if (GameSaver.Instance.HasSavedLocation()) {
        //     transform.position = GameSaver.Instance.GetSavePointLocation();
        //     transform.rotation = GameSaver.Instance.GetSavePointRotation();
        // }
    }

    public void Die() {
        // TODO: change player state
        // TODO: wait a bit
        onDie.Invoke();
    }

    public void Hit() {
        // TODO: change player state
        // TODO: wait a bit
        onHit.Invoke();
    }

}
