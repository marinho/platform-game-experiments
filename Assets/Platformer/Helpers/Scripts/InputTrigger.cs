using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onAnyInput;
    [SerializeField] List<string> inputButtons;

    void Update()
    {
        foreach (var inputButton in inputButtons)
        {
            if (Input.GetButtonUp(inputButton)) {
                onAnyInput.Invoke();
                break;
            }
        }
    }
}
