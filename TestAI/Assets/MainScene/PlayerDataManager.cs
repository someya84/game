using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{

    public int player1Data, player2Data, player3Data, player4Data;
    
    [HideInInspector]
    public List<int> playersDatas = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDatas(int type, int mat)
    {
        playersDatas.Add((type + 1) * 100 + (mat + 1));
    }
}
