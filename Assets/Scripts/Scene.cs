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
    }

    /// <summary>
    /// RetryButtonが押されたとき
    /// </summary>
    public void OnClickRetry()
    {
        SceneManager.LoadScene("Stage");
        //Phaseを戻す必要がある
    }

    [SerializeField] GameObject StrengtheningPanel;

    /// <summary>
    /// 強化ボタンが押されたとき
    /// </summary>
    public void OnClickStrengthening()
    {
        StrengtheningPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 強化Panelから戻るとき
    /// </summary>
    public void OnClickReturnStr()
    {
        StrengtheningPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
