using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;

    public int movedCheckCount;
    const int movedCheckMacCount = 3;
    public GameObject eventArea;

    // Start is called before the first frame update
    void Start()
    {
        movedCheckCount = 0;
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        eventArea.SetActive(true);
    }

    public bool CheckTask()
    {
        if (movedCheckCount >= movedCheckMacCount)
        {
            return true;
        }
        return false;
    }

    public float GetTransitionTime() { return 2f; }
}
