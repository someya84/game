using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //0-->プレイヤー
    //1-->NPC１
    //2-->NPC２
    //3-->NPC３
    public int player1Score,player2Score,player3Score,player4Score;

    public int[] playerScores;

    // Start is called before the first frame update
    void Start()
    {
        player1Score = 0;
        player2Score = 0;
        player3Score = 0;
        player4Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerScores = new int[] { player1Score, player2Score, player3Score, player4Score };
    }

    public void AddScore(int playerNum, int _count)
    {
        switch (playerNum)
        {
            case 0:
                player1Score += _count;
                if (player1Score < 0)
                {
                    player1Score = 0;
                }
                break;
            case 1:
                player2Score += _count;
                if (player2Score < 0)
                {
                    player2Score = 0;
                }
                break;
            case 2:
                player3Score += _count;
                if (player3Score < 0)
                {
                    player3Score = 0;
                }
                break;
            case 3:
                player4Score += _count;
                if (player4Score < 0)
                {
                    player4Score = 0;
                }
                break;
        }
        //スコアの変動があったら、スコア表示の並び順を変えるスクリプト（PanelChange.ButtonClick）にアクセス
    }
}
