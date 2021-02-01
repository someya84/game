using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public GameObject player;
    NattoItemTask nattoItemTask;
    PepperItemTask pepperTask;
    const float defmovespeed = 0.1f;
    float moveSpeed;

    bool nattoHit;
    float taskTime;
    float taskEndTime = 3;

    Vector3 nowPos;
    float loopmoveTime;

    public GameObject targetMarker;

    [SerializeField] int MoveSpeed;
    [SerializeField] float chargeTime;

    //ランダム移動
    private bool randomMove;
    private float timeCount;//0
    private float randomMoveTimer;
    private const float randomMoveTime = 5f;

    //タスク切り替えフラグ
    float changeTime;
    bool changeTimeStart;

    //移動（反復）
    public Transform[] destPoints;
    Vector3 targetDestPos;
    int pointCounter;

    // Start is called before the first frame update
    void Start()
    {
        nattoItemTask = player.GetComponent<NattoItemTask>();
        pepperTask = player.GetComponent<PepperItemTask>();
        moveSpeed = defmovespeed;
        taskTime = 0;
        nattoHit = false;
        loopmoveTime = 0;
        targetMarker.SetActive(false);
        this.gameObject.SetActive(false);
        randomMove = false;
        changeTime = 0;
        changeTimeStart = false;

        SetDestination();
        pointCounter = 0;
    }

    void Update()
    {
        RandomMove();
        TaskChange();
    }

    //炎に当たった時の移動処理
    void RandomMove()
    {
        if (randomMove)
        {
            timeCount += Time.deltaTime;
            randomMoveTimer += Time.deltaTime;
            if (randomMoveTimer >= randomMoveTime)
            {
                timeCount = 0;
                randomMoveTimer = 0;
                randomMove = false;
                changeTimeStart = true;
                return;
            }
            // 自動で前進する。
            transform.position += (transform.forward * MoveSpeed) * Time.deltaTime;
            // 指定した時間を経過すると、
            if (timeCount > chargeTime)
            {

                // 進路をランダムに変更する。
                Vector3 course = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
                transform.localRotation = Quaternion.Euler(course);
                // タイムカウントを0に戻す。
                timeCount = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (randomMove) return;
        NattoTaskCheck();
        DefMove();
    }

    //炎に当たった後にタスクを完了させる
    void TaskChange()
    {
        if (changeTimeStart)
        {
            changeTime += Time.deltaTime;
            if (changeTime >= 1)
            {
                pepperTask.catchItem = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    //通常移動
    //2つのポジションを反復移動
    void DefMove()
    {
        var dir = targetDestPos - transform.position;
        if(dir.magnitude < 1f)
        {
            SetDestination();
        }

        transform.rotation
            = Quaternion.Slerp(transform.rotation,
               Quaternion.LookRotation(dir), 0.3f);
        transform.position += transform.forward * moveSpeed;
    }

    //目的地の設定
    void SetDestination()
    {
        targetDestPos = destPoints[pointCounter % destPoints.Length].transform.position;
        pointCounter++;
    }

    //納豆に当たった後にタスク完了に切り替え
    void NattoTaskCheck()
    {
        if (nattoHit)
        {
            taskTime += Time.deltaTime;
            if (taskTime >= taskEndTime)
            {
                moveSpeed = defmovespeed;
                TaskEnd();
                nattoHit = false;
            }
        }
    }

    //納豆が当たった時の数値変更処理
    public void HitNatto()
    {
        nattoHit = true;
        moveSpeed = moveSpeed / 2;
        targetMarker.SetActive(false);
    }

    //納豆タスクの終了
    void TaskEnd()
    {
        nattoItemTask.colNatto = true;
    }

    //納豆で選ばれたときにターゲットマーカーの表示
    public void TargetedOn()
    {
        targetMarker.SetActive(true);
        targetMarker.transform.position =
            new Vector3(transform.position.x,
            targetMarker.transform.position.y,
            transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Flame":
                if (!randomMove)
                {
                    player.GetComponent<TutorialPlayer>().firing = false;
                    randomMove = true;
                }
                break;
        }
    }
}
