using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAIController : MonoBehaviour
{
    private AIController aIController;

    void Start()
    {
        aIController = GetComponentInParent<AIController>();
    }

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Fruit")
        {
            //　敵キャラクターの状態を取得
            AIController.EnemyState state = aIController.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state != AIController.EnemyState.Chase)
            {
                Debug.Log("プレイヤー発見");
                aIController.SetState(AIController.EnemyState.Chase, col.transform);
            }
        }
        //フルーツが消えたらステータス変更
        if (aIController.fruitTransform == null)
        {
            Debug.Log("フルーツを取られた");
            //aIController.fruitTransform;
            aIController.SetState(AIController.EnemyState.Wait);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Fruit")
        {
            Debug.Log("見失う");
            aIController.SetState(AIController.EnemyState.Wait);
        }
    }
}
