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
        PlayerPrefs.SetString(GameSaverConsts.LatestPositionChapter, chapterId);
    }

    public void SetSavePointLocation(Vector2 position)
    {
        PlayerPrefs.SetFloat(GameSaverConsts.LatestPositionLocation + ".position.x", position.x);
        PlayerPrefs.SetFloat(GameSaverConsts.LatestPositionLocation + ".position.y", position.y);
    }

    public void SetSavePointRotation(Quaternion rotation)
    {
        PlayerPrefs.SetFloat(GameSaverConsts.LatestPositionLocation + ".rotation.y", rotation.eulerAngles.y);
    }

    public Vector2 GetSavePointLocation()
    {
        return new Vector2(
            PlayerPrefs.GetFloat(GameSaverConsts.LatestPositionLocation + ".position.x"),
            PlayerPrefs.GetFloat(GameSaverConsts.LatestPositionLocation + ".position.y")
        );
    }

    public Quaternion GetSavePointRotation()
    {
        var y = PlayerPrefs.GetFloat(GameSaverConsts.LatestPositionLocation + ".rotation.y");
        return Quaternion.Euler(0f, y, 0f);
    }

}

public static class GameSaverConsts {
    public static string LatestPositionChapter = "LatestPosition.Chapter";
    public static string LatestPositionLocation = "LatestPosition.Location";
}
