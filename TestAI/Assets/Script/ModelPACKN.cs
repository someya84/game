using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModelPACKN : Model
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //colors = new Material[3,2];
        //parts = new GameObject[2];
        colors[0] = AssetDatabase.LoadAssetAtPath("Assets/Materials/PACKN/PacknFace1.mat", typeof(Material)) as Material;
        colors[1] = AssetDatabase.LoadAssetAtPath("Assets/Materials/PACKN/PacknFace2.mat", typeof(Material)) as Material;
        colors[2] = AssetDatabase.LoadAssetAtPath("Assets/Materials/PACKN/PacknFace3.mat", typeof(Material)) as Material;
        parts = transform.Find("Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
