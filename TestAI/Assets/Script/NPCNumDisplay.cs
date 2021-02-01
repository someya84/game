using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCNumDisplay : MonoBehaviour
{
    public Sprite[] NumberImage;
    int PcNum;
    int NPcNum = 3;
    Image image;
    const int UpperLimit = 3;
    const int LowerLimit = 1;
    public GameObject UpButton;
    public GameObject DownButton;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        SetImage();
        UpButton.SetActive(NPcNum != UpperLimit);
        DownButton.SetActive(NPcNum != LowerLimit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetImage() {
        image.sprite = NumberImage[NPcNum - 1];
    }
    public void ChangeNum(bool Up) {
        bool ok = false;
        if (Up)
        {
            if (NPcNum < UpperLimit)
            {
                ok = true;
                NPcNum++;
            }
        }
        else {
            if (NPcNum > LowerLimit)
            {
                ok = true;
                NPcNum--;
            }
        }
        SetImage();
        ShowNumber(ok);
        UpButton.SetActive(NPcNum != UpperLimit);
        DownButton.SetActive(NPcNum != LowerLimit);
    }
    public (int, int) GetNum() {
        return (PcNum, NPcNum);
    }
    void ShowNumber(bool ok) {
        string Result = "";
        if (ok)
        {
            switch (NPcNum)
            {
                case 1:
                    Result = "One!";
                    break;
                case 2:
                    Result = "Two!";
                    break;
                case 3:
                    Result = "Three!";
                    break;
                default:
                    break;
            }
        }
        else {
            switch (NPcNum) {
                case 1:
                    Result = "Zer...o？";
                    break;
                case 3:
                    Result = "Fou...この構成には、何かが足りない";
                    break;
                default:
                    break;
            }
        }
        Debug.Log(Result);
    }
}
