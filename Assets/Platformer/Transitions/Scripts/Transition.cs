using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime;
    [SerializeField] UnityEvent onTransitionStart;
    [SerializeField] UnityEvent onTransitionMiddle;
    [SerializeField] UnityEvent onTransitionEnd;

    public void Play() {
        StartCoroutine(TriggerAnimatorAndInvokeEvent());
    }

    IEnumerator TriggerAnimatorAndInvokeEvent() {
        transition.SetBool("isPlaying", true);
        onTransitionStart.Invoke();
        yield return new WaitForSeconds(transitionTime / 2);
        onTransitionMiddle.Invoke();
        yield return new WaitForSeconds(transitionTime / 2);
        onTransitionEnd.Invoke();
        transition.SetBool("isPlaying", false);
    }
}
