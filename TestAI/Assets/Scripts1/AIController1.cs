using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AIController1 : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack
    };
    //　歩くスピード
    [SerializeField]
    private float walkSpeed;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private EnemyState state;
    //　フルーツのTransform
    public GameObject fruitPrefab = null;

    private float searchTime = 0;    //経過時間

    //　移動方向
    private Vector3 direction;

    //　速度
    private Vector3 velocity;

    private Animator animator;

    private Vector3 targetPos;

    //目的地
    private Vector3 destination;

    [SerializeField] private GameObject target;


    // Use this for initialization
    void Start()
    {
        walkSpeed = 5.0f;
        waitTime = 2.0f;
        animator = GetComponent<Animator>();
        elapsedTime = 0f;
        setPosition = GetComponent<SetPosition>();
        SetState(EnemyState.Wait);
        fruitPrefab = serchTag(gameObject, "Fruit");
        velocity = Vector3.zero;
        SetDestination(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        //経過時間を取得
        searchTime += Time.deltaTime;
        waitTime = RandomTime();
        if (searchTime >= waitTime)
        {
            Debug.Log("ok");
            //最も近かったオブジェクトを取得
            //fruitPrefab = serchTag(gameObject, "Fruit");
            SetState(EnemyState.Chase);
            //SetState(EnemyState.Walk);
            //経過時間を初期化
            searchTime = 0;
        }

        //　見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {

            animator.SetFloat("RunFloat", 2.0f);

            if (fruitPrefab != null)
            {

                if (state == EnemyState.Chase)
                {

                }

                //direction = (new Vector3(fruitPrefab.transform.position.x,0,fruitPrefab.transform.position.z) - transform.position).normalized;

                //対象の位置の方向を向く
                //transform.LookAt(new Vector3(fruitPrefab.transform.position.x, gameObject.transform.position.y, fruitPrefab.transform.position.z));
                //transform.LookAt(new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z));

                //自分自身の位置から相対的に移動する
                //transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
                //rigidbody.velocity = new Vector3(direction.x * walkSpeed, transform.position.y, direction.z * walkSpeed);
            }
            else 
            {
                    SetState(EnemyState.Wait);

            }

            velocity = Vector3.zero;
            direction = (new Vector3(GetDestination().x, transform.position.y, GetDestination().z) - transform.position).normalized;
            transform.LookAt(new Vector3(GetDestination().x, transform.position.y, GetDestination().z));
            velocity = direction * walkSpeed;
            transform.position += velocity * Time.deltaTime;

            Debug.Log(Vector3.Distance(transform.position, new Vector3(GetDestination().x, transform.position.y, GetDestination().z)));
            if (Vector3.Distance(transform.position,new Vector3(GetDestination().x,transform.position.y,GetDestination().z)) < 0.5f)
            {
                Debug.Log("arrive");
                SetState(EnemyState.Wait);
                //vectorPos = Vector3.zero;

            }
            else if (state == EnemyState.Wait)
            {

                elapsedTime += Time.deltaTime;
                if (elapsedTime > 1.0f)
                {
                    SetState(EnemyState.Walk);
                }

            }
        }
    }

    //　敵キャラクターの状態変更メソッド
    public void SetState(EnemyState tempState)
    {
        if (tempState == EnemyState.Walk)
        {
            state = tempState;
            elapsedTime = 0f;
            targetPos = CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            state = tempState;

            //　追いかける対象をセット
            fruitPrefab = serchTag(gameObject, "Fruit");
            //targetPos = new Vector3(fruitPrefab.transform.position.x, transform.position.y, fruitPrefab.transform.position.z);
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;
            arrived = true;
            velocity = Vector3.zero;
            //animator.SetFloat("Speed", 0f);
            animator.SetFloat("RunFloat", 0f);
        }
    }

    public GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
                                    //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }

        if(targetObj != null)
        {
            //targetPos = new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z);
            SetDestination(targetObj.transform.position);
        }

        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    public GameObject InstanceThrowNatto(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
                                    //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }

        if (targetObj.tag == "tagName" && targetObj.gameObject != this.gameObject)
        {
            /*
            GameObject Natto = Instantiate(throwNatto,
                                    new Vector3(this.transform.position.x,
                                    this.transform.position.y + 4,
                                    this.transform.position.z
                                    ), Quaternion.identity);
                                    */
        }

            return targetObj;
    }

    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }

    //　ランダムな位置の作成
    public Vector3 CreateRandomPosition()
    {
        //ランダムなVector2の値を得る
        Vector2 randDestination = Random.insideUnitCircle * 5;
        //float randDestination = Random.Range(-10.0f, 10.0f);

        //現在地にランダムな位置を足して目的地とする
        //targetPos = new Vector3(randDestination.x, transform.position.y, randDestination.y);
        //return new Vector3(randDestination, transform.position.y, randDestination);
        return new Vector3(randDestination.x, transform.position.y, randDestination.y);

    }

    public float RandomTime()
    {
        return Random.Range(1.5f,2.5f);
    }

    //　目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
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
