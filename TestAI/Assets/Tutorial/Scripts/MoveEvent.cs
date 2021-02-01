using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    public GameObject haveManagerObject;
    MovementTask moveCheck;
    KickTask kickTask;

    int phase;
    public List<Vector3> position;

    const int maxEnterCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        moveCheck = haveManagerObject.GetComponent <MovementTask> ();
        kickTask = haveManagerObject.GetComponent<KickTask>();
        this.transform.position = position[0];
    }

    //次に目指す場所を変更
    void ViewEventArea(int _phase)
    {
        if (_phase >= position.Count)
        {
            this.gameObject.SetActive(false);
            kickTask.CanNextTask();
            return;
        }
        this.transform.position = position[_phase];
    }

    //指定位置に到達したかどうか
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveCheck.movedCheckCount++ ;

            if (phase >= maxEnterCount) { return; }
            phase++;
            ViewEventArea(phase);
        }
    }
}
