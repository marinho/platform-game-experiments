using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformerCamera : MonoBehaviour
{
    [SerializeField] UnityEvent onMovedTo;
    [SerializeField] float speed;
    [SerializeField] bool animatedMove;

    bool isMoving = false;
    Vector3 movingToPosition;
    bool isFirstRendering = true;

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

    void OnRenderObject() {
        // if (isFirstRendering) {
        //     isFirstRendering = false;
        //     var room = PlayerMovement.Instance.GetCurrentRoom();
        //     MoveToRoom(room.transform.position);
        // }
    }

}
