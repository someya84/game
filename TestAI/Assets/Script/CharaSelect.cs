using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CharaSelect : SelectMode
{
    //enum Status //現在の選択肢の場所
    //{
    //    Model_Select,
    //    Color_Select,
    //    Check_Select,
    //}
    //Status status = Status.Model_Select;
    //public Button[] button;
    //GameObject selecter = null;
    public EventSystem eventsystem;
    public GameObject[] Chara;
    Transform Charatransform;
    //public Material characolor;
    public GameObject ColorPalettes;
    //int charanum = 0;
    //GameObject Charas;
    public GameObject frame;
    float posz;
    GameObject Selecter;//ゲーム画面に渡す用(GameObject)
    public Material Selecter_Color;//ゲーム画面に渡す用(使用するMaterial)
    int SelecterCode;
    int SelecterMaterialCode;
    public GameObject NpcNumbers;
    int NpuNumber;
    int PcNumber;
    // Start is called before the first frame update
    void Start()
    {
        Selecter = null;
        Selecter_Color = null;
        //CharaActive(charanum);
        //ColorChange(characolor);
        posz = Chara[0].transform.position.z;
        ColorPalettes.SetActive(false);
        PcNumber = 1;
        SetScript();
        // SelectChara(Chara[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //表示しているキャラクターを回転させる
        for (int i = 0; i < Chara.Length; i++)
        {
            Charatransform = Chara[i].transform;
            Charatransform.Rotate(new UnityEngine.Vector3(0, 10 * (1.0f / 60.0f), 0));
        }
        //if (status == Status.Model_Select) {  
        //}
        //CheckKey();
        //CheckSelection();
    }
    //ボタンのフォーカスが変更したかをチェックし、変更している場合変更先の内容を反映させる
    //void CheckSelection() { 
    //    if (selecter != eventsystem.currentSelectedGameObject  && eventsystem.currentSelectedGameObject != null) {
    //            selecter = eventsystem.currentSelectedGameObject;
    //        if (selecter.GetComponent<SelectElements>() != null)
    //        {
    //            SelectElements elements = selecter.GetComponent<SelectElements>();
    //            elements.Select();
    //        }
    //    }
    //}
    //キーボードの状態をチェックする
    //void CheckKey() { 
    //    if (Input.GetKeyDown(KeyCode.Escape)) {
    //        switch (status) {
    //            case Status.Color_Select:
    //                status = Status.Model_Select;
    //                //button[(int)Status.Model_Select].Select();
    //                break;
    //            case Status.Check_Select:
    //                status = Status.Color_Select;
    //                button[(int)Status.Color_Select].Select();
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
    //プレイヤーのマテリアルを引数先のマテリアルに変更する。
    //引数：変更する色(Material)
    //void ColorChange(Material Color) {
    //    //characolor = Color;
    //    for (int i = 0; i < Chara.Length; i++)
    //    {
    //        Model model = Chara[i].GetComponent<Model>();
    //        //model.ColorSelect();
    //        //Renderer CharaRenderer = Chara[i].GetComponent<Renderer>();
    //        //CharaRenderer.material = characolor;
    //    }
    //}
    public void ColorSelect(int colornum) {
        Model model = Selecter.GetComponent<Model>();
        model.ColorSelect(colornum);
        Selecter_Color = model.Color(colornum);
        SelecterMaterialCode = colornum;
        //  ColorChange(Color);
    }
    //右に表示するキャラクターを変更する。
    //配列内の引数に対応しているキャラクターを表示し、それ以外のキャラを非表示にする。
    //変更前から角度、色を引き継ぐ
    //引数：表示するキャラクターに対応している配列内の数字
    public void CharaChange(int num) {
        //UnityEngine.Quaternion chararotation = Charatransform.rotation;
        //charanum = num;
        //ColorChange(characolor);
        //Charatransform = Chara[charanum].transform;
        //Charatransform.rotation = chararotation;
        //CharaActive(charanum);
    }
    //選択肢を次に飛ばす
    //public void CurrentSelect() {
    //        switch (status)
    //        {
    //            case Status.Model_Select:
    //            status = Status.Color_Select;
    //            button[(int)Status.Color_Select].Select();
    //                break;
    //            case Status.Color_Select:
    //            status = Status.Check_Select;
    //                button[(int)Status.Check_Select].Select();
    //                break;
    //        default:
    //            break;
    //        }
    //}

    //画面内に表示するキャラクターを設定する
    //引数に対応しているキャラクターのActiveをtrueにし、それ以外をfalseにする
    //引数：表示するキャラクターに対応している配列内の数字
    //void CharaActive(int num) {
    //    for (int i = 0; i < Chara.Length; i++) {
    //        if (i == charanum)
    //        {
    //            Chara[i].SetActive(true);
    //        }
    //        else {
    //            Chara[i].SetActive(false);
    //        }
    //    }
    //}
    public void SelectCheck() {
        if (SelecterCode != 99)
        {
            Debug.Log("キャラ選択完了！");
            selectmaneger.SavePlayer(SelecterCode, SelecterMaterialCode);
            var (pcnum, npcnum) = GetNPCNumber();
            selectmaneger.SetCharaNum(pcnum,npcnum);
            FinishSelect();
        }
        else
        {
            Debug.Log("キャラクター選択が完了していません！");

        }
    }
    //
    public void OnPointerClick(GameObject gameobject) {
        Debug.Log(gameobject.name + "がクリックされた！");
        SelectChara(gameobject);
        //CurrentSelect();
    }
    //Chara内に引数と一致しているobjectがある場合、そのobjectを前に移動させる、
    //一致していない場合、最初の位置に移動させる
    //引数：選んだ対象のgameObject
    void SelectChara(GameObject gameobject) {
        for (int i = 0; i < Chara.Length; i++) {
            UnityEngine.Vector3 charapos = Chara[i].transform.position;
            if (Chara[i] == gameobject)
            {
                Model charamodel = gameobject.GetComponent<Model>();
                float selectposx = posz - charamodel.frontdistance;
                Chara[i].transform.position = new UnityEngine.Vector3(charapos.x, charapos.y, selectposx);
                Selecter = Chara[i];
                SelecterCode = i;
                //Debug.Log("変更前:" + charapos.z);
                //Debug.Log("変更後:" + Chara[i].transform.position.z);
                
                //色が選択されていない場合、色を設定する
                //if (Selecter_Color == null) {
                //    ColorSelect(0);
                //}
                ColorPalette colorpalette = ColorPalettes.GetComponent<ColorPalette>();
                colorpalette.ImageChange(i);
            }
            else
            {
                float nonselectposz = posz;
                Chara[i].transform.position = new UnityEngine.Vector3(charapos.x, charapos.y, nonselectposz);
            }
        }
        ColorPalettes.SetActive(true);
    }
    public (int, int) GetNPCNumber() {
        NPCNumDisplay display = NpcNumbers.GetComponent<NPCNumDisplay>();
        var (pcnum, npcnum) = display.GetNum();
        return (pcnum, npcnum);
    }
}