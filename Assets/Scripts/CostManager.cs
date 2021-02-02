using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    int m_cost= 4;
    /// <summary>
    /// プロパティ
    /// </summary>
    public int Cost { get { return m_cost; } }

    /// <summary> コストのTextオブジェクト</summary>
    [SerializeField] GameObject m_costObject;
    /// <summary> コストを表示するテキスト</summary>
    Text m_costText;

    void Start()
    {
        m_costText = m_costObject.GetComponent<Text>();
        m_costText.text = "コスト : " + m_cost;
    }

    /// <summary>
    /// コストが増えたときの関数
    /// </summary>
    public void UpCost()
    {
        //コストを1増やす
        m_cost++;
        m_costText.text = "コスト : " + m_cost;
    }
    /// <summary>
    /// コストが減らしたときの関数
    /// </summary>
    public void DecreaseCost()
    {
        m_cost--;
        m_costText.text = "コスト : " + m_cost;
    }
}
