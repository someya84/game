using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;

    bool moved;
    public bool movetaskDone;

    void Start()
    {
        moved = false;
        movetaskDone = false;
    }

    public void CanNextTask()
    {
        movetaskDone = true;
    }

    public void Animated()
    {
        if (movetaskDone)
        {
            moved = true;
        }
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting() { }

    public bool CheckTask()
    {
        if (moved)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetTransitionTime() { return 2f; }
}
