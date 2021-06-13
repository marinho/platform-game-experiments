using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
class EditorHelper : EditorWindow
{
    static EditorHelper()
    {
        EditorApplication.update += Update;
    }

    [MenuItem("Play/PlayMe _F8")]
    public static void RunMainScene()
    {
        if (EditorApplication.isPlaying) {
            return;
        }

        string currentSceneName = EditorSceneManager.GetActiveScene().name;
        File.WriteAllText(".lastScene", currentSceneName);

        EditorSceneManager.OpenScene(GetScenePath("SampleScene"));
        EditorApplication.EnterPlaymode();
    }
    
    [MenuItem("Play/StopMe _#F8")]
    public static void StopPlaying()
    {
        if (!EditorApplication.isPlaying) {
            return;
        }

        EditorApplication.ExitPlaymode();
        hasToLoadPreviousSceneWhenStopPlaying = true;
    }

    static int count = 0;
    static bool hasToLoadPreviousSceneWhenStopPlaying = false;
    static void Update()
    {
        if (hasToLoadPreviousSceneWhenStopPlaying) {
            if (++count > 500) {
                count = 0;
                if (!EditorApplication.isPlaying) {
                    hasToLoadPreviousSceneWhenStopPlaying = false;
                    string lastScene = File.ReadAllText(".lastScene");
                    EditorSceneManager.OpenScene(GetScenePath(lastScene));
                }
            }
        }
    }

    static string GetScenePath(string sceneName) {
        return "Assets/Scenes/" + sceneName + ".unity";
    }

}