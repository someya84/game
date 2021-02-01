using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palette : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    Color RightColor;
    Color DarkColor;
    // Start is called before the first frame update
    void Start()
    {
        //DarkColor = new Color(1.0f,1.0f, 1.0f, 1.0f);
        //RightColor = new Color(255.0f, 255.0f, 255.0f,1.0f);
        DarkColor = Color.gray;
        RightColor = Color.white;
        image = GetComponent<Image>();
        image.color = DarkColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColorChange(int num) {
        image.sprite = sprites[num];
    }
    public void OutImage() {
        image.color = DarkColor;
    }
    public void EnterImage()
    {
        image.color = RightColor;

    }
}
