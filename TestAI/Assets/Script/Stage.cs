using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Waku;
    void Start()
    {
        Waku = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetWaku() {
        return Waku;
    }
}
