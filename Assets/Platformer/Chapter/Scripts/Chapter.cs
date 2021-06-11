using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    [SerializeField] string chapterId;

    void Start()
    {
        GameSaver.Instance.SetCurrentChapter(chapterId);
    }

}
