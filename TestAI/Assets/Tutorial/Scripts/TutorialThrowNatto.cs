using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialThrowNatto : MonoBehaviour
{
    GameObject target;

    void Start()
    {
    }

    // Start is called before the first frame update
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation
            = Quaternion.Slerp(transform.rotation,
               Quaternion.LookRotation(target.transform.position - transform.position), 0.3f);
        transform.position += transform.forward * 0.02f;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("!!!");
            other.gameObject.GetComponent<TutorialEnemy>().HitNatto();
        }
    }
}
