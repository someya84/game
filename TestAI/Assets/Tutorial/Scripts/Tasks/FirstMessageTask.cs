using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMessageTask : MonoBehaviour,ITutorialTask
{
    public string title;
    public string text;
    bool nextCheck;
    public GameObject nextTextButton;

    void Start()
    {
        nextTextButton.SetActive(true);
        nextCheck = false;
    }

    // Start is called before the first frame update
    public void ButtonClick()
    {
        nextCheck = true;
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting() { }

    public bool CheckTask()
    {
        if(nextCheck)
        {
            nextTextButton.SetActive(false);
            return true;
        }
        return false;
    }

    public float GetTransitionTime() { return 2f; }
}
