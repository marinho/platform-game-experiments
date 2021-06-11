using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : Singleton<PlayerRespawn>
{
    // Prevent non-singleton constructor use.
    protected PlayerRespawn() { }

    void Start() {
        Respawn();
    }

    public void Respawn() {
        transform.position = GameSaver.Instance.GetSavePointLocation();
        transform.rotation = GameSaver.Instance.GetSavePointRotation();
    }

}
