using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack
    };

    private CharacterController enemyController;

    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 3.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 2f;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private EnemyState state;
    //　フルーツのTransform
    public Transform fruitTransform;

    private bool check = true;

    // Use this for initialization
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
        Debug.Log(fruitTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //　見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                setPosition.SetDestination(fruitTransform.position);
            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                //animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, this.transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 0.5f)
            {
                SetState(EnemyState.Wait);
                //animator.SetFloat("Speed", 0.0f);
            }
            //　到着していたら一定時間待つ
            else if (state == EnemyState.Wait)
            {
                elapsedTime += Time.deltaTime;

                //　待ち時間を越えたら次の目的地を設定
                if (elapsedTime > waitTime)
                {
                    SetState(EnemyState.Walk);
                }
            }

            //フルーツが消えたらステータス変更
            //if (fruitTransform == null)
            //{
            //    SetState(EnemyState.Wait);
            //}
            //if (GameManager.instance.getFruit)
            //{
            //    SetState(EnemyState.Wait);
            //    GameManager.instance.getFruit = false;
            //}

            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);
        }

    }

    //　敵キャラクターの状態変更メソッド
    public void SetState(EnemyState tempState, Transform targetObj = null)
    {
        if (tempState == EnemyState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            state = tempState;
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            fruitTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector3.zero;
            //animator.SetFloat("Speed", 0f);
        }
    }
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            Debug.Log("Get");
            Destroy(other.gameObject);
            SetState(EnemyState.Wait);
            GameManager.instance.getFruit = true;
            //fruitTransform = this.transform;
        }
    }

}
