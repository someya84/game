using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NattoItemTask : MonoBehaviour, ITutorialTask
{
    public string title;
    public string text;
    public bool colNatto;

    public GameObject natto;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        natto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetTitle() { return title; }
    public string GetText() { return text; }

    public void OnTaskSetting()
    {
        natto.SetActive(true);
        enemy.SetActive(true);
    }

    public bool CheckTask()
    {
        if (colNatto)
        {
            return true;
        }
        return false;
    }

    public float GetTransitionTime() { return 2f; }
}
