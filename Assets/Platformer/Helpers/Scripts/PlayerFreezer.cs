using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezer : MonoBehaviour
{
    void OnEnable()
    {
        PlayerMovement.Instance.DisableMovements();
    }

    void OnDisable()
    {
        PlayerMovement.Instance.EnableMovements();
    }
}
