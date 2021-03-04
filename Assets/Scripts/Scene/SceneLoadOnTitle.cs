using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadOnTitle : MonoBehaviour
{
    public void OnClickStage1()
    {
        SceneManager.LoadScene("Stage");
    }

    [SerializeField] GameObject m_howToPlay;

    public void OnClickHowToPlay()
    {
        m_howToPlay.SetActive(true);
    }

    public void OnClickHowToPlayBack()
    {
        m_howToPlay.SetActive(false);
    }
}
