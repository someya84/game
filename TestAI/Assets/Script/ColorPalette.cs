using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public GameObject[] palettes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImageChange(int num) {
        for (int i = 0; i < palettes.Length; i++) {
            Palette palette = palettes[i].GetComponent<Palette>();
            palette.ColorChange(num);
        }
    }
}
