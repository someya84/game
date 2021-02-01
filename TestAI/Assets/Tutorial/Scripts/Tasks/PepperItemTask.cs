using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperItemTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public bool catchItem;

    public GameObject pepper;

    // Start is called before the first frame update
    void Start()
    {
        pepper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        pepper.SetActive(true);
    }

    public bool CheckTask()
    {
        if (catchItem)
        {
            return true;
        }
        return false;
    }

    public float GetTransitionTime() { return 2f; }
}
