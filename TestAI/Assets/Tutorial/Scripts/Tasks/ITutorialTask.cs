public interface ITutorialTask
{
    ///<summary>
    /// チュートリアルのタスクタイトル
    ///</summary>
    ///<return></return>
    string GetTitle();

    ///<summary>
    /// 説明文を取得
    ///</summary>
    ///<return></return>
    string GetText();

    ///<summary>
    /// タスクが設定されたときに実行
    ///</summary>
    ///<return></return>
    void OnTaskSetting();

    ///<summary>
    /// タスクが達成されたか判断
    ///</summary>
    ///<return></return>
    bool CheckTask();

    ///<summary>
    /// 達成後に次のタスクへ遷移するまでの時間
    ///</summary>
    ///<return></return>
    float GetTransitionTime();

}
