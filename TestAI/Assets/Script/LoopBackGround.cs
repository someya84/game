using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopBackGround : MonoBehaviour
{
    public float speedX = 0.1f;//縦軸の移動の速さ
    public float speedY = 0.1f;//横軸の移動の速さ
    public GameObject BG;//ループさせる画像を格納するGameObject
    public Sprite[] BGImages;//ループさせる画像(現在４つまで)
    //GameObject[,] LoopBG = new GameObject[2,2];//ループするのに必要な背景(斜め移動なので４つ)
    GameObject[,] LoopBG;
    RectTransform[,] BGTransforms;
    int BGNum;
    //RectTransform[,] BGTransforms = new RectTransform[2, 2];//LoopBGに対応しているRectTransform
    // Start is called before the first frame update
    void Start()
    {
        if (BGImages.Length == 1)
        {
            BGNum = 2;
        }
        else {
            BGNum = BGImages.Length;
        }
        LoopBG = new GameObject[BGNum, BGNum];//ループするのに必要な背景(斜め移動なので４つ)
        BGTransforms = new RectTransform[BGNum, BGNum];

        for (int i = 0; i < BGNum; i++) {
            for (int j = 0; j < BGNum; j++) {
                if (j >= i - 1 && j <= i + 1)//画面に映る部分のみ
                {
                    //GameObjectの生成
                    LoopBG[i, j] = Instantiate(BG, new Vector3(Screen.width * i, Screen.height * j, 0), Quaternion.identity);
                    LoopBG[i, j].transform.SetParent(this.transform, false);//生成したGameObjectを子Objectに設定
                    BGTransforms[i, j] = LoopBG[i, j].GetComponent<RectTransform>();//RectTransformを取得
                    Image image = LoopBG[i, j].GetComponent<Image>();
                    image.sprite = BGImages[i % BGImages.Length];//表示する画像を変更
                }
            }
        }
        ////GameObjectの生成
        //LoopBG[0,0] = Instantiate(BG,new Vector3(0,0,0),Quaternion.identity);
        //LoopBG[0,1] = Instantiate(BG, new Vector3(0, Screen.height, 0), Quaternion.identity);
        //LoopBG[1,1] = Instantiate(BG, new Vector3(Screen.width,0, 0), Quaternion.identity);
        //LoopBG[1,0] = Instantiate(BG, new Vector3(Screen.width, Screen.height, 0), Quaternion.identity);
        //for (int i = 0; i < 2; i++) {
        //    for (int j = 0; j < 2; j++) {
        //        LoopBG[i, j].transform.SetParent(this.transform, false);//生成したGameObjectを子Objectに設定
        //        BGTransforms[i, j] = LoopBG[i, j].GetComponent<RectTransform>();//RectTransformを取得
        //        Image image = LoopBG[i, j].GetComponent<Image>();
        //        image.sprite = BGImages[(i + j) % BGImages.Length];//表示する画像を変更
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BGNum; i++)
        {
            for (int j = 0; j < BGNum; j++)
            {
                if (j >= i - 1 && j <= i + 1)//画面に映る部分のみ
                {
                    BGTransforms[i, j].anchoredPosition -= new Vector2(speedX, speedY);//画像の移動
                    if (BGTransforms[i, j].anchoredPosition.x < -Screen.width)//画像の横軸が画面端に到達した場合
                    {
                        //画像を右端に移動させる
                        BGTransforms[i, j].anchoredPosition += new Vector2(Screen.width * 2, 0);
                        //BGTransforms[i, j].anchoredPosition = new Vector2(Screen.width , BGTransforms[i,j].anchoredPosition.y);
                        //BGTransforms[i, j].anchoredPosition = new Vector2(Screen.width + BGTransforms[i,(j + 1) % 2].anchoredPosition.x, BGTransforms[i, j].anchoredPosition.y);
                    }
                    if (BGTransforms[i, j].anchoredPosition.y < -Screen.height)//画像の縦軸が画面端に到達した場合
                    {
                        //画像を上端に移動させる
                        BGTransforms[i, j].anchoredPosition += new Vector2(0, Screen.height * BGNum);
                        //BGTransforms[i, j].anchoredPosition = new Vector2(BGTransforms[i,j].anchoredPosition.x, Screen.height);
                        //BGTransforms[i, j].anchoredPosition = new Vector2(BGTransforms[i, j].anchoredPosition.x, Screen.height + BGTransforms[(i+ 1) % 2, j].anchoredPosition.y);
                    }

                }
            }
        }
    }
}
