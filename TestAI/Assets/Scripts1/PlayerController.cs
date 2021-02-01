using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; //プレイヤーの動くスピード
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度

    [SerializeField] private float x; //x方向のImputの値
    [SerializeField] private float z; //z方向のInputの値

    [SerializeField] private Vector3 moveVelocity;

    private Rigidbody rigd;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
    }

    void Update()
    {
        /*
        //RunFloat > 0 RunFloat < 1
        x = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
        z = Input.GetAxis("Vertical"); //z方向のInputの値を取得

        moveVelocity = new Vector3(x, 0, z).normalized;

        rigd.velocity = new Vector3(moveVelocity.x * speed, 0, moveVelocity.z * speed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動

        if (rigd.velocity.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                      Quaternion.LookRotation(rigd.velocity),
                                      applySpeed);
        }

        animator.SetFloat("RunFloat", rigd.velocity.magnitude);
        */

        
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

        Debug.Log(moveVelocity.magnitude);
        

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ok");
            animator.SetBool("BlessFire", true);
            StartCoroutine("BlessFireCoroutine");
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            Debug.Log("Get");
            Destroy(other.gameObject);
        }
    }

    private IEnumerator BlessFireCoroutine()
    {

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("BlessFire", false);
    }
}
