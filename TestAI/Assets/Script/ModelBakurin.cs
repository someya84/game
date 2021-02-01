using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModelBakurin : Model
{
    Texture[] textures;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        colors[0] = AssetDatabase.LoadAssetAtPath("Assets/Materials/Flog/T_bakurin/Bakurin1.mat", typeof(Material)) as Material;
        colors[1] = AssetDatabase.LoadAssetAtPath("Assets/Materials/Flog/T_bakurin/Bakurin2.mat", typeof(Material)) as Material;
        colors[2] = AssetDatabase.LoadAssetAtPath("Assets/Materials/Flog/T_bakurin/Bakurin3.mat", typeof(Material)) as Material;
        parts = GameObject.Find("geo 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
