using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectJugment : MonoBehaviour
{
    int m_stageJudgment;

    /// <summary>
    /// どのステージが選択されたか判断するためのプロパティ
    /// </summary>
    public int StageJudgment
    {
        set { m_stageJudgment = value; }
        get { return m_stageJudgment; }
    }
}
