using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItemTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public GameObject apple;
    public GameObject banana;

    public int fruitCount;
    const int maxItemCount = 2;

    void Start()
    {
        apple.SetActive(false);
        banana.SetActive(false);
    }

    public void AddFruitCount()
    {
        fruitCount++;
        //Debug.Log(fruitCount);
    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting() {
        apple.SetActive(true);
        banana.SetActive(true);
    }

    public bool CheckTask()
    {
        if (fruitCount >= maxItemCount)
        {
            return true;
        }
            return false;
    }

    public float GetTransitionTime() { return 2f; }
}
