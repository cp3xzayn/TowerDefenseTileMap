using UnityEngine;

public class BeforeLoadScene
{
    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        var manager = GameObject.Instantiate(Resources.Load("StageSelectManager"));
        //シーンをまたいでもこのオブジェクトは破壊されないようにする
        GameObject.DontDestroyOnLoad(manager);
    }
}
