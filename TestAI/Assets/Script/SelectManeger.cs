using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManeger : MonoBehaviour
{
    // Start is called before the first frame update
    
     //選択画面のシーン状況
    enum Status_Select { 
        Status_Stage,
        Status_Player,
    }
    Status_Select status;
    public float time = 5f;
    bool finish;
    int count;
    public GameObject[] ActiveMode;//表示するGameObject()
    int PlayerCode;
    int PlayerMaterialCode;
    int StageCode;
    int NpcNumber;
    int PcNumber;
    void Start()
    {
        finish = false;
        status = Status_Select.Status_Stage;//最初の画面
        ChangeMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (finish) {
            time -= Time.deltaTime;
            if (count != (int)time)
            {
                Debug.Log(count + "・・・");
                count = (int)time;
            }
            if (time <= 0) {
                Debug.Log("ゲーム画面に移行します。");
                SceneManager.sceneLoaded += GameSceneLoaded;
                SceneManager.LoadScene("MainScene");
                //ここからシーン遷移で行う動作を記入する
                //問題点:Player,Stage共に同一モデルながら別のオブジェクトを参照する可能性大、このままメインゲームシーンにPlayer,Stageを渡すとバグが間違いなく起こると思われる
                //解決策1:選択画面内ではほかの人が付けたスクリプトを外して自分のスクリプトをつけ、このタイミングで元に戻して渡す。
                //解決策2:同一モデルでスクリプトを変えた二種類のプレハブを用意し、このタイミングで同じモデルのステージ用のプレハブをメインゲームシーンに渡す。
                //どちらの解決策の場合でも相談が必要不可欠。

                finish = false;
            }
        }
    }


    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //Find(MainSceneのGameManager)
        GameObject.Find("GameManager").GetComponent<GetDataManager>().GetPlayersNum(0,0,0,0);
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    //選択画面の変更
    //現在のstatusから表示する
    void ChangeMode() { 
        for (int i = 0; i < ActiveMode.Length; i++) {
                ActiveMode[i].SetActive(i == (int)status);
        }
    }
    public void NextSelect() {
        switch (status) {
            case Status_Select.Status_Player:
                FinishSelect();//選択を終了する
                break;
            case Status_Select.Status_Stage:
                status = Status_Select.Status_Player;
                break;
            default:
                break;
        }
        ChangeMode();
    }
    public void ReturnSelect()
    {
        switch (status)
        {
            case Status_Select.Status_Player:
                status = Status_Select.Status_Stage;
                break;
            case Status_Select.Status_Stage:
                ReturnSelect();//タイトルに戻る
                break;
            default:
                break;
        }
        ChangeMode();
    }
    //選択を終了し、キャラクターの情報、ステージの情報をメイン画面に移す
    void FinishSelect() {
        finish = true;
        count = (int)time;
    }
    void ReturnTitle() {
        SceneManager.LoadScene("TitleScene");
    }
    public void SavePlayer(int player,int playerColor) {
        PlayerCode = player;
        PlayerMaterialCode = playerColor;
    }
    public void SaveStage(int stage) {
        StageCode = stage;
    }
    public void SetCharaNum(int PcNum, int NpcNum) {
        PcNumber = PcNum;
        NpcNumber = NpcNum;
    }
}
