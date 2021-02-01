using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TutorialItemScript : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
