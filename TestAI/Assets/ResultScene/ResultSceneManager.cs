using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    //[HideInInspector]
    public List<int> playerScores;

    [SerializeField]
    public List<int> playersNum;

    //オブジェクトとマテリアルのデータを三桁の数字で管理
    //100の位--->オブジェクトデータの種類
    //1の位--->マテリアルデータの種類
    [SerializeField]
    public List<int> playersData;

    //----------------モデルとマテリアル
    public GameObject[] modelType;
    public Material[] modeltype1Materials;
    public Material[] modeltype2Materials;


    //歴代ハイスコア表示をする場合に使用
    //private int highScore;

    //得点の高いプレイヤーが二人以上いるか
    private bool ishighScorePlayers;

    public Text[] playersScoreText;

    public GameObject[] playersDispPos;
    public GameObject winnerPos;

    //生成したオブジェクトを仮置き
    GameObject[] players;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[4];

        ishighScorePlayers = false;

        PlayersObjSet();
        ShowScore();
        SortScores();
        ScoreJudgment();
        ImageDisp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayersObjSet()
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            int type = playersData[i] / 100 - 1;
            int mat = playersData[i] % 100 - 1;

            GameObject player =
                Instantiate(modelType[type], playersDispPos[i].transform.position,
                Quaternion.identity);
            switch (type)
            {
                case 0:
                    player.GetComponent<Renderer>().material = modeltype1Materials[mat];
                    break;
                case 1:
                    player.GetComponent<Renderer>().material = modeltype2Materials[mat];
                    break;
                default:
                    break;
            }
            players[i] = player;
        }
    }

    /// <summary>
    /// スコアの表示
    /// </summary>
    void ShowScore()
    {
        for (int i = 0; i < playersNum.Count; i++)
        {
            //Debug.Log(i);
            playersScoreText[i].text = playerScores[i].ToString();
        }
    }

    /// <summary>
    /// 貰ったデータをバブルソートで昇順に並び替え
    /// 同時にプレイヤーを得点の高い順に変える
    /// </summary>
    void SortScores()
    {
        for (int i = playersNum.Count - 1; i > 0; i--)
        {
            for (int j = i - 1; j >= 0; j--)
            {
                if (playerScores[i] > playerScores[j])
                {
                    int s = playerScores[j];
                    playerScores[j] = playerScores[i];
                    playerScores[i] = s;

                    int n = playersNum[j];
                    playersNum[j] = playersNum[i];
                    playersNum[i] = n;
                }
            }
        }
    }

    /// <summary>
    /// 同点者がいるか判断
    /// いるならカウントを増やす
    /// </summary>
    void ScoreJudgment()
    {
        for (int i = 1; i < playersNum.Count; i++)
        {
            if (playerScores[i - 1] == playerScores[i])
            {
                ishighScorePlayers = true;
                break;
            }
        }

        Debug.Log(ishighScorePlayers);
    }

    /// <summary>
    /// 結果による画像の表示
    /// </summary>
    void ImageDisp()
    {
        //勝者を前に出す
        players[playersNum[0]].transform.position = winnerPos.transform.position;



        if (!ishighScorePlayers)
        {
            //勝者画像表示
        }
        else
        {
            //引き分け画像を表示
        }
    }
}
