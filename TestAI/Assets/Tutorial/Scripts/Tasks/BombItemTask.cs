using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItemTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public bool catchItem;

    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        bomb.SetActive(true);
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
