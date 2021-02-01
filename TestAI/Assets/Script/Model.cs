using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public int num;
    public float frontdistance;//画面手前に飛び出す距離
    protected Material[] colors;
    protected GameObject parts;
    protected CharaSelect charaselect;
    virtual protected void Start()
    {
        colors = new Material[3];
    }
    public void Select()
    {
        Debug.Log("キャラ変更！");
        charaselect.CharaChange(num);
    }
    //色を変更する
    public void ColorSelect(int num) {
        Renderer part = parts.GetComponent<Renderer>();
        part.material = colors[num];
    }
    public Material Color(int num) {
        return colors[num];
    }
}
