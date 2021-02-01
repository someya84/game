using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    int players = 4;
    ScoreManager scoreManager;
    GetDataManager getDataManager;
    PlayerDataManager playerDataManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
        getDataManager = GetComponent<GetDataManager>();
        playerDataManager = GetComponent<PlayerDataManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("ResultScene");
    }

    //シーンを移動したときに、スコアデータ、
    //キャラのオブジェクトタイプ、マテリアルデータを次のシーンに引き継ぐ
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var resultManager = GameObject.Find("ResultSceneManager").GetComponent<ResultSceneManager>();
        resultManager.playerScores.Clear();
        resultManager.playersNum.Clear();
        resultManager.playersData.Clear();

        for (int i = 0; i < getDataManager.totalPlayersNum; i++)
        {
            //オブジェクトデータとマテリアルデータを配列の形でセット
            resultManager.playerScores.Add(scoreManager.playerScores[i]);
            resultManager.playersNum.Add(i);
            resultManager.playersData.Add(playerDataManager.playersDatas[i]);
        }

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
