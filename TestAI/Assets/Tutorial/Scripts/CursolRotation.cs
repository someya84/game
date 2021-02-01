using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

            Vector3 dir = hit.point - this.transform.position;
            Quaternion rotaiton = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
            transform.rotation = rotaiton;
            }
    }
}
