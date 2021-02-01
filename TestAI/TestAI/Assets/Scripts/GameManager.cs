﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //シングルトン用
    static public GameManager instance;
    //アイテムプレハブ
    public GameObject[] ItemPrefab;
    //時間間隔の最小値
    public float minTime = 0.5f;
    //時間間隔の最大値
    public float maxTime = 1f;
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


    /// <summary>
    /// シングルトン化
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
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
        return Random.Range(minTime, maxTime);
    }

    private int GetRandomItemID()
    {
        return Random.Range(0, 3);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        //float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, 30, z);
    }

    
}