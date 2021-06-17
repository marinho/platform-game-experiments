using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Chapter : MonoBehaviour
{
    [SerializeField] string chapterId;
    [SerializeField] public Transform initialPosition;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onEnd;

    Transform latestPlayerLocation;

    void Start()
    {
        onStart.Invoke();
        // GameSaver.Instance.SetCurrentChapter(chapterId);
    }

    public void EndChapter() {
        onEnd.Invoke();
    }

    public void LoadNextChapter() {
        // GameSaver.Instance.UnsetSavePointLocation();
        var thisScene = SceneManager.GetActiveScene();
        LoadScene(thisScene.buildIndex + 1);
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public static Chapter GetForScene(Scene scene) {
        var sceneChildren = scene.GetRootGameObjects();
        foreach (var item in sceneChildren)
        {
            Chapter chapter = item.GetComponent<Chapter>();
            if (chapter != null) {
                return chapter;
            }
        }
        return null;
    }

    public void SetLatestLocation(Transform latestLocation) {
        latestPlayerLocation = latestLocation;
    }

    public Transform GetLatestLocation() {
        return latestPlayerLocation;
    }

}
