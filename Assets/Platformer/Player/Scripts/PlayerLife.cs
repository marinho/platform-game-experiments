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
        } else {
            SetLocationToChapterInitialPosition();
        }
        // if (GameSaver.Instance.HasSavedLocation()) {
        //     transform.position = GameSaver.Instance.GetSavePointLocation();
        //     transform.rotation = GameSaver.Instance.GetSavePointRotation();
        // }
    }

    void MoveTo(Vector2 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetLocationToChapterInitialPosition() {
        var chapter = Chapter.GetForScene(gameObject.scene);
        if (chapter.initialPosition != null) {
            MoveTo(chapter.initialPosition.position, chapter.initialPosition.rotation);
        }
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
