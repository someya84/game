using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// ゲーム上のチュートリアルを管理するマネージャークラス
/// </summary>
public class TutorialManager : MonoBehaviour
{
    //チュートリアル用UI
    public Text tutorialTitle;
    public Text tutorialText;

    //チュートリアルタスク
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    //チュートリアルタスクの条件を満たした際の遷移用フラグ
    private bool task_executed = false;


    void Start()
    {
        //チュートリアルの一覧
        tutorialTask = new List<ITutorialTask>()
        {
            this.gameObject.GetComponent<FirstMessageTask>(),
            this.gameObject.GetComponent<MovementTask>(),
            //this.gameObject.GetComponent<KickTask>(),
            this.gameObject.GetComponent<FruitItemTask>(),
            this.gameObject.GetComponent<FeverFruitItemTask>(),
            this.gameObject.GetComponent<NattoItemTask>(),
            this.gameObject.GetComponent<PepperItemTask>(),
            this.gameObject.GetComponent<BombItemTask>(),
            this.gameObject.GetComponent<LastMessageTask>(),
        };

        //最初のチュートリアルを設定
        StartCoroutine(SetCurrentTask(tutorialTask.First()));
    }

    // Update is called once per frame
    void Update()
    {
        //チュートリアルが存在し実行されていない場合に処理
        if(currentTask != null && !task_executed)
        {
            //現在のチュートリアルが実行されたか判定
            if (currentTask.CheckTask())
            {
                task_executed = true;

                tutorialTask.RemoveAt(0);

                var nextTask = tutorialTask.FirstOrDefault();
                if(nextTask != null)
                {
                    StartCoroutine(SetCurrentTask(nextTask, 0.5f));
                }
            }
        }
    }

    ///<summary>
    ///新しいチュートリアルタスクを設定する
    /// </summary>
    /// <param name="task"></param>
    /// <return></return>
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {
        //timeが設定されている場合は待機
        yield return new WaitForSeconds(time);

        currentTask = task;
        task_executed = false;

        //UIにタイトルと説明文を設定
        tutorialTitle.text = task.GetTitle();
        tutorialText.text = task.GetText();

        //チュートリアルタスク設定時用の関数を実行
        task.OnTaskSetting();
    }
}
