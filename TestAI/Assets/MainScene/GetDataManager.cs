using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDataManager : MonoBehaviour
{
    //----------------プレイヤーのモデルとマテリアル
    public GameObject[] playerType;
    public GameObject[] enemyType;
    public Material[] playertype1Materials;
    public Material[] playertype2Materials;

    //----------------他のモデルと被らないようにするために管理
    [SerializeField]
    private bool[,] combinations;

    //---------------プレイヤーの人数とモデル・マテリアルの種類と敵の人数
    int playersNum;
    int playerTypeData;
    int playerMaterialData;
    int enemyPlayersNum;

    public int totalPlayersNum;

    //--------------マテリアルデータの保存
    PlayerDataManager _playerDataManager;

    //--------------ゲーム開始時の出現場所
    public GameObject[] spawnPoints;

    //プレイヤーが選択したモデル、マテリアルデータをセット
    public void GetPlayersNum(int _playersNum, int _enemyNum, int _type, int _material)
    {
        playersNum = _playersNum;
        enemyPlayersNum = _enemyNum;
        playerTypeData = _type - 1;
        playerMaterialData = _material - 1;
        totalPlayersNum = playersNum + enemyPlayersNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerDataManager = GetComponent<PlayerDataManager>();
        GetPlayersNum(1,3,1,2);

        SetCombinations();
        PlayerSet();
        EnemySet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //オブジェクトとマテリアルのセットの組み合わせを初期化
    void SetCombinations()
    {
        combinations = new bool[playerType.Length,playertype1Materials.Length];
        for (int i = 0; i < playerType.Length; i++)
        {
            for (int j = 0; j < playertype1Materials.Length; j++)
            {
                combinations[i, j] = false;
            }
        }
    }

    //プレイヤーのオブジェクトとマテリアルをセット
    void PlayerSet()
    {
        GameObject Player = Instantiate(playerType[playerTypeData], spawnPoints[0].transform.position, Quaternion.identity);
        switch (playerTypeData)
        {
            case 0:
            Player.GetComponent<Renderer>().material = playertype1Materials[playerMaterialData];
                break;
            case 1:
            Player.GetComponent<Renderer>().material = playertype2Materials[playerMaterialData];
                break;
            default:
                break;
        }
        combinations[playerTypeData, playerMaterialData] = true;
        Player.name = "Player1";
        _playerDataManager.SetDatas(playerTypeData, playerMaterialData);
    }

    //残りの敵のオブジェクトとマテリアルを組み合わせる
    void EnemySet()
    {
        for (int i = 0; i < enemyPlayersNum; i++)
        {
            int x = 0, y = 0;
            do
            {
                x = Random.Range(0,2);
                y = Random.Range(0, 3);
            } while (combinations[x,y]);
            GameObject Enemy = Instantiate(enemyType[x], spawnPoints[i + playersNum].transform.position, Quaternion.identity);
            switch (x)
            {
                case 0:
                Enemy.GetComponent<Renderer>().material = playertype1Materials[y];
                    break;
                case 1:
                    Enemy.GetComponent<Renderer>().material = playertype2Materials[y];
                    break;
                default:
                    break;
            }
            combinations[x,y] = true;
            Enemy.name = "Player" + (i + 1 + playersNum).ToString();
            _playerDataManager.SetDatas(x,y);
        }
    }
}
