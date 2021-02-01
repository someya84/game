using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StageSelect : SelectMode
{
    public GameObject[] stages;
    Transform[] stagetransforms;
    int SelecterCode;
    public Material SelectMaterial;
    public Material NonSelectMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
        stagetransforms = new Transform[stages.Length];
        for (int i = 0; i < stages.Length; i++) {
            stagetransforms[i] = stages[i].transform;
        }
        SelecterCode = 99;//99 = 未入力
        SetScript();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stagetransforms.Length; i++) { 
            //stagetransforms[i].Rotate(new UnityEngine.Vector3(0, 10 * (1.0f / 60.0f), 0));
        }
    }
    public void OnPointerClick(GameObject gameobject)
    {

        Debug.Log(gameobject.name + "がクリックされた！");
        SelectStage(gameobject);

    }
    public void SelectCheck()
    {
        if (SelecterCode != 99)
        {
            Debug.Log("ステージ選択完了！");
            SetScript();
            selectmaneger.SaveStage(SelecterCode);
            FinishSelect();
        }
        else
        {
            Debug.Log("ステージ選択が完了していません！");

        }
    }
    void SelectStage(GameObject selecter) {
        for (int i = 0; i < stages.Length; i++) {
            Material material;
            if (stages[i] == selecter)
            {
                material = SelectMaterial;
                SelecterCode = i;
            }
            else
                material = NonSelectMaterial;
            Stage stage = stages[i].GetComponent<Stage>();
            MeshRenderer waku = stage.GetWaku().GetComponent<MeshRenderer>();
            waku.material = material;
        }
    }
}
