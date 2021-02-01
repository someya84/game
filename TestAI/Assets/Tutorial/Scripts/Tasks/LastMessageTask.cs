using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMessageTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public GameObject startButton;

    void Start()
    {
        startButton.SetActive(false);
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        startButton.SetActive(true);
    }

    public bool CheckTask()
    {
        return false;
    }

    public float GetTransitionTime() { return 2f; }
}
