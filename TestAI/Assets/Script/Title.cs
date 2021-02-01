using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    enum ButtonCommand {
        Start,
        Tutorial,
        Continue,
        ButtonNum
    }
    public GameObject[] selection;
    int selectMaxNum;
    int selectNum;
    float selectColor;
    // Start is called before the first frame update
    void Start()
    {
        //selectNum = 0;
        selectMaxNum = selection.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckKey();
        //ColorChange();

    }
    void CheckKey() {

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ResetColor();
            selectNum = (selectNum + (selectMaxNum - 1)) % selectMaxNum;
            //Debug.Log(selectNum);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ResetColor();
            selectNum = (selectNum + 1) % selectMaxNum;
            //Debug.Log(selectNum);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            EnterButton(selectNum);
        }
    }

    void EnterButton(int num) {
        switch (num) {
            case 0:
                NewGame();
                break;
            case 1:
                Continue();
                break;
            case 2:
                Option();
                break;
            default:
                break;
        }
    }
    void ResetColor() {
        Color selectcolor = new Color(255, 255, 255, 255);
        selection[selectNum].GetComponent<Image>().color = selectcolor;
        //selection[selectNum].GetComponentInChildren<Text>().color = selectcolor;
        selectColor = 0.0f;
    }
    void ColorChange() {
        Color selectcolor = new Color(255, 255, 255, Mathf.Sin(selectColor) * 255);
        selection[selectNum].GetComponent<Image>().color = selectcolor;
        //selection[selectNum].GetComponentInChildren<Text>().color = selectcolor;
        selectColor = selectColor+ (1.0f / 60.0f) * 00.5f;
        if (selectColor > 360.0f) {
            selectColor -= 360.0f;
        }
    }
    public void Continue()
    {
        SceneManager.LoadScene("CharaSelectScene");
        Debug.Log("続きから！");
    
    }
    public void NewGame()
    {
        //SceneManager.LoadScene("NewGameScene");
        Debug.Log("初めから！");

    }
    public void Option()
    {
        //SceneManager.LoadScene("NewGameScene");
        Debug.Log("せってい！");

    }
}
