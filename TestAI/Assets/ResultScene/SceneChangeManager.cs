using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public string changeSceneName;

    public void ToNextScene()
    {
        SceneManager.LoadScene(changeSceneName);
    }
}
