using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadOnTitle : MonoBehaviour
{
    /// <summary> どのステージが選択されたかを判定する </summary>
    StageSelectJugment sJudgment;

    void Start()
    {
        sJudgment = GameObject.Find("StageSelectManager(Clone)").GetComponent<StageSelectJugment>();
    }

    public void OnClickStage1()
    {
        sJudgment.StageJudgment = 1;
        SceneManager.LoadScene("Stage");
    }

    public void OnClickStage2()
    {
        sJudgment.StageJudgment = 2;
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
