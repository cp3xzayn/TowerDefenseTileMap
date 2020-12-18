using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHpManager : MonoBehaviour
{
    /// <summary> 拠点の耐久値 </summary>
    int m_baseHP = 20;
    /// <summary>最初の拠点の耐久値 </summary>
    int m_decidedHP;
    [SerializeField] Slider m_slider;

    void Start()
    {
        m_slider.value = 1;
        m_decidedHP = m_baseHP;
    }
    void Update()
    {
        //拠点の耐久値が0以下になったら
        if (m_baseHP <= 0)
        {
            GameOver();
        }
    }
    /// <summary>
    /// 拠点の耐久値を減らす関数
    /// </summary>
    /// <param name="eneAttack"></param>
    public void DecreaseHP(int eneAttack)
    {
        m_baseHP -= eneAttack;
        m_slider.value = (float)m_baseHP / (float)m_decidedHP;
        Debug.Log("拠点の耐久値" + m_baseHP);
    }
    /// <summary>
    /// GameStateをGameOverに変える関数
    /// </summary>
    void GameOver()
    {
        GameManager.Instance.SetNowState(GameState.GameOver);
    }
}
