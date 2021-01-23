using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
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
}
