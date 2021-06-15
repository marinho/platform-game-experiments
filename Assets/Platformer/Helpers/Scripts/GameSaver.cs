using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : Singleton<GameSaver>
{
    string currentChapterId;

    // Prevent non-singleton constructor use.
    protected GameSaver() { }

    public void SetCurrentChapter(string chapterId)
    {
        currentChapterId = chapterId;
        SetString(GameSaverConsts.LatestPositionChapter, chapterId);
    }

    public void SetSavePointLocation(Vector2 position)
    {
        SetFloat(GameSaverConsts.LatestPositionLocation + ".position.x", position.x);
        SetFloat(GameSaverConsts.LatestPositionLocation + ".position.y", position.y);
        SetBool(GameSaverConsts.LatestPositionLocation + ".hasPosition", true);
    }

    public void SetSavePointRotation(Quaternion rotation)
    {
        SetFloat(GameSaverConsts.LatestPositionLocation + ".rotation.y", rotation.eulerAngles.y);
    }

    public void UnsetSavePointLocation()
    {
        DeleteKey(GameSaverConsts.LatestPositionLocation + ".position.x");
        DeleteKey(GameSaverConsts.LatestPositionLocation + ".position.y");
        DeleteKey(GameSaverConsts.LatestPositionLocation + ".rotation.y");
        DeleteKey(GameSaverConsts.LatestPositionLocation + ".hasPosition");
    }

    public bool HasSavedLocation() {
        return GetBool(GameSaverConsts.LatestPositionLocation + ".hasPosition");
    }

    public Vector2 GetSavePointLocation()
    {
        return new Vector2(
            GetFloat(GameSaverConsts.LatestPositionLocation + ".position.x"),
            GetFloat(GameSaverConsts.LatestPositionLocation + ".position.y")
        );
    }

    public Quaternion GetSavePointRotation()
    {
        var y = GetFloat(GameSaverConsts.LatestPositionLocation + ".rotation.y");
        return Quaternion.Euler(0f, y, 0f);
    }

    void SetFloat(string key, float value) {
        PlayerPrefs.SetFloat(key, value);
    }

    float GetFloat(string key) {
        return PlayerPrefs.GetFloat(key);
    }

    void SetString(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }

    string GetString(string key) {
        return PlayerPrefs.GetString(key);
    }

    void SetBool(string key, bool value) {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    bool GetBool(string key) {
        return PlayerPrefs.GetInt(key) == 1;
    }

    void DeleteKey(string key) {
        PlayerPrefs.DeleteKey(key);
    }

}

public static class GameSaverConsts {
    public static string LatestPositionChapter = "LatestPosition.Chapter";
    public static string LatestPositionLocation = "LatestPosition.Location";
}
