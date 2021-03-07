using UnityEngine;

public class BeforeLoadScene
{
    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        var stageSelectmanager = GameObject.Instantiate(Resources.Load("StageSelectManager"));
        var soundManager = GameObject.Instantiate(Resources.Load("SoundManager"));
        //シーンをまたいでもこのオブジェクトは破壊されないようにする
        GameObject.DontDestroyOnLoad(stageSelectmanager);
        GameObject.DontDestroyOnLoad(soundManager);
    }
}
