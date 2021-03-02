using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    /// <summary>
    /// TitleButtonが押されたとき
    /// </summary>
    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
        //Phaseを戻すための処理
        GameManager.Instance.SetNowState(GameState.Start);
    }

    /// <summary>
    /// RetryButtonが押されたとき
    /// </summary>
    public void OnClickRetry()
    {
        SceneManager.LoadScene("Stage");
        //Phaseを戻すための処理
        GameManager.Instance.SetNowState(GameState.Start);
    }

}
