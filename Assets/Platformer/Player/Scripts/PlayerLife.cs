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
        transform.position = GameSaver.Instance.GetSavePointLocation();
        transform.rotation = GameSaver.Instance.GetSavePointRotation();
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
