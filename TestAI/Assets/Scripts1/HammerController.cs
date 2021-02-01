using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    private float collitionValue;
    // Start is called before the first frame update
    void Start()
    {
        collitionValue = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, -1, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();
            // 衝突相手はRigidbodyをアタッチした立方体で、別途空から降らせる

            Vector3 impulse = (rigid.position - transform.parent.position).normalized * this.collitionValue;
            // 衝突相手の座標から回転中心の座標(頭部から見てハンマー本体は親なので、transform.parent.positionで親の位置をとる)を引き、正規化してimpulseMagnitudeをかける
            // ※衝突相手とハンマーはどちらもシーンの一番上層にあるので、それらの座標がそのままワールド座標になる

            rigid.AddForce(impulse, ForceMode.Impulse);
            // 瞬間的な衝撃をかける場合(力積を加える場合)はForceMode.Impulseを使う
            // ※ちなみにVelocityChangeを使うと衝突相手の速度を直接変えられる
            //  今回は衝突相手の質量に応じて吹き飛び量を変えたかったため、Impulseとした
        }

    }
}
