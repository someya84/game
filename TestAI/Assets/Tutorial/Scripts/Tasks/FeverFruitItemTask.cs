using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverFruitItemTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public bool catchItem;

    public GameObject feverFruit;

    // Start is called before the first frame update
    void Start()
    {
        feverFruit.SetActive(false);
        catchItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        feverFruit.SetActive(true);
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
