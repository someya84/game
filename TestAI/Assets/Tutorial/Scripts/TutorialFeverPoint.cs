using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFeverPoint : MonoBehaviour
{
    public GameObject[] item = { };
    float timer;
    float totalTiemr;
    const float feverTime = 3f;

    public GameObject Player;
    FeverFruitItemTask feverFruitTask;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        totalTiemr = 0f;

        feverFruitTask = Player.GetComponent<FeverFruitItemTask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTiemr >= feverTime)
        {
            feverFruitTask.catchItem = true;
            this.gameObject.SetActive(false);
        }

        timer += Time.deltaTime;
        totalTiemr += Time.deltaTime;

        if (timer >= 0.5f)
        {
            timer = 0f;
            FeverTime();
        }
    }

    void FeverTime()
    {
        int n = Random.Range(0,item.Length);
        int x = Random.Range(-2, 3);
        int z = Random.Range(-2, 3);
        GameObject Item = Instantiate(item[n], new Vector3(x, transform.position.y, z), Quaternion.identity);
    }
}
