using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //シングルトン用
    public static  GameManager instance;
    //アイテムプレハブ
    public GameObject[] ItemPrefab;
    //時間間隔の最小値
    public float minTime = 2f;
    //時間間隔の最大値
    public float maxTime = 5f;
    //X座標の最小値
    public float xMinPosition = -5f;
    //X座標の最大値
    public float xMaxPosition = 5f;
    //Y座標の最小値
    public float yMinPosition = 0f;
    //Y座標の最大値
    public float yMaxPosition = 10f;
    //Z座標の最小値
    public float zMinPosition = -5f;
    //Z座標の最大値
    public float zMaxPosition = 5f;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    public bool getFruit;

    //円周率
    public float pi = 3.1415f;

    //大円の半径
    public float bigCircle = 20.0f;

    //大円の穴の半径
    public float holeCircle = 10.0f;


    /// <summary>
    /// シングルトン化
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject Item = Instantiate(ItemPrefab[GetRandomItemID()]);

            //生成した敵の位置をランダムに設定する
            Item.transform.position = GetRandomPosition();
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //次に発生する時間間隔を決定する
            interval = GetRandomTime();


        }

    }

    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        //return Random.Range(minTime, maxTime);
        return Random.Range(0.5f, 1f);
    }

    private int GetRandomItemID()
    {
        return Random.Range(0, 3);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //Vector2 instanceFruitPosition = Random.insideUnitCircle * 8;
        //それぞれの座標をランダムに生成する
        float randRadius = Random.Range(0f, Mathf.PI * 2);
        float direction = Random.Range(5.0f, 10.0f);

        float x = Mathf.Sin(randRadius) * direction;
        float z = Mathf.Cos(randRadius) * direction;

        //Vector3型のPositionを返す
        return new Vector3(x, 30, z);
        //return new Vector3(instanceFruitPosition.x, 30, instanceFruitPosition.y);
    }


}