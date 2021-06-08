using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformerCamera : Singleton<PlatformerCamera>
{
    [SerializeField] UnityEvent onMovedTo;
    [SerializeField] float speed;
    [SerializeField] bool animatedMove;

    bool isMoving = false;
    Vector3 movingToPosition;

    // Prevent non-singleton constructor use.
    protected PlatformerCamera() { }

    public void MoveToRoom(Vector3 position) {
        if (animatedMove) {
            movingToPosition = position;
            isMoving = true;
        } else {
            transform.position = position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                movingToPosition,
                speed * Time.deltaTime
            );

            if (transform.position.x == movingToPosition.x && transform.position.y == movingToPosition.y) {
                StopMoving();
            }
        }
    }

    void StopMoving() {
        isMoving = false;
        onMovedTo.Invoke();
    }

}
