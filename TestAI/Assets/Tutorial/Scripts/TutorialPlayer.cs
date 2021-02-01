using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{

    [SerializeField] private Vector3 moveVelocity;

    Rigidbody rb;
    FruitItemTask fruitTask;
    BombItemTask bombTask;
    private Animator animator;

    //フィーバーフルーツ（ドリアン）のときに使用
    public GameObject feverPoint;

    //納豆の処理時に使用
    bool getNatto;
    GameObject target;
    public GameObject directionMarker;
    public GameObject throwNatto;

    //移動処理
    const float defspeed = 5.0f;
    float speed;
    float moveX = 0f;
    float moveZ = 0f;
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度

    //ペッパーの処理に使用
    public GameObject fireEffectCol;
    public bool firing;

    //爆弾に当たった時に使用
    float bomerTimer;
    bool stan;
    Color defcolor;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        fruitTask = GetComponent<FruitItemTask>();
        bombTask = GetComponent<BombItemTask>();
        rend = GetComponent<Renderer>();
        defcolor = rend.material.color;

        speed = defspeed;

        feverPoint.SetActive(false);

        getNatto = false;
        directionMarker.SetActive(false);

        fireEffectCol.SetActive(false);

        firing = false;

        stan = false;
    }

    // Update is called once per frame
    void Update()
    {
        Stan();

        //敵に当たったら炎が消えるようにする（チュートリアル用）
        if (!firing)
        {
            fireEffectCol.SetActive(false);
        }

        moveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVelocity.z += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVelocity.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVelocity.z -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity.x += 1;
        }

        moveVelocity = moveVelocity.normalized * speed * Time.deltaTime;

        if (moveVelocity.magnitude > 0)
        {
            transform.position += moveVelocity;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                      Quaternion.LookRotation(moveVelocity),
                                      applySpeed);
        }

        animator.SetFloat("RunFloat", moveVelocity.magnitude);

        ThrowNatto();
    }

    //爆弾に当たった時の処理
    void Stan()
    {
        if (stan)
        {
            //点滅
            bomerTimer += Time.deltaTime;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 3));
            rend.material.color = new Color(level, level, level, level);
            if (bomerTimer >= 3)
            {
                bombTask.catchItem = true;
                rend.material.color = defcolor;
                speed = defspeed;
                stan = false;
            }
        }
    }

    //納豆を投げる処理
    void ThrowNatto()
    {
        if (getNatto)
        {
            if (Input.GetMouseButtonDown(0))
            {
                target = null;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    target = hit.collider.gameObject;
                }

                //Debug.Log(target);
                //ターゲット（他プレイヤー）だった場合は、納豆が生成後、
                //クリックしたオブジェクトに向かって進むようにする
                if (target.tag == "Player" && target.gameObject != this.gameObject)
                {
                    target.GetComponent<TutorialEnemy>().TargetedOn();
                    GameObject Natto = Instantiate(throwNatto,
                     new Vector3(this.transform.position.x,this.transform.position.y + 4,
                                    this.transform.position.z), Quaternion.identity);
                    Natto.GetComponent<TutorialThrowNatto>().SetTarget(target);
                    getNatto = false;
                    directionMarker.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Melon":
            case "Banana":
                fruitTask.AddFruitCount();
                other.gameObject.SetActive(false);
                break;
            case "Dorian":
                feverPoint.SetActive(true);
                other.gameObject.SetActive(false);
                break;
            case "Natto":
                other.gameObject.SetActive(false);
                getNatto = true;
                directionMarker.SetActive(true);
                break;
            case "Pepper":
                if (!firing)
                {
                    firing = true;
                    fireEffectCol.SetActive(true);
                    animator.SetBool("BlessFire", true);
                    StartCoroutine("BlessFireCoroutine");
                }
                other.gameObject.SetActive(false);
                break;
            case "Bomb":
                bomerTimer = 0;
                speed = 0f;
                stan = true;
                other.gameObject.SetActive(false);
                break;
        }
    }

    private IEnumerator BlessFireCoroutine()
    {

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("BlessFire", false);
    }
}