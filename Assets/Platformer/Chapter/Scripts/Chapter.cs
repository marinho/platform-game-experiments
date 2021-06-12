using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Chapter : MonoBehaviour
{
    [SerializeField] string chapterId;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onEnd;

    void Start()
    {
        onStart.Invoke();
        GameSaver.Instance.SetCurrentChapter(chapterId);
    }

    public void EndChapter() {
        onEnd.Invoke();
    }

    public void LoadNextChapter() {
        var thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }

}
