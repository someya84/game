using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCState
    {
        Walk,
        Wait,
        GetFruit,
        Chase
    };

    //目的地
    private Vector3 destination;

    //歩くスピード
    [SerializeField]
    private float walkSpeed = 3.0f;

    //速度
    private Vector3 velocity;

    //移動方向
    private Vector3 direction;

    //到着フラグ 
    private bool arrived;

    //　経過時間
    private float elapsedTime;

    // 敵の状態
    private NPCState state;

    //　プレイヤーTransform
    private Transform playerTransform;
}
