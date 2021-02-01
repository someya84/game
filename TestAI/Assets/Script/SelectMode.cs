using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : MonoBehaviour
{
    public GameObject Maneger;
    protected SelectManeger selectmaneger;
    //// Start is called before the first frame update
    void Start()
    {

    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    //選択が完了した際に呼ぶ関数
    protected void FinishSelect() {
        Debug.Log("選択項目移行");
        selectmaneger.NextSelect();
    }
    public void ReturnSelect()
    {
        Debug.Log("選択項目を戻る");
        selectmaneger.ReturnSelect();
    }
    protected void SetScript() { 
        selectmaneger = Maneger.GetComponent<SelectManeger>();
        
    }
}
