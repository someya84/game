using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMaterial : MonoBehaviour
{
    public int colornum;
    public Sprite[] Image;
    protected CharaSelect charaselect;
    public void Select()
    {
        charaselect.ColorSelect(colornum);
    }

}
