using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHpManager : MonoBehaviour
{
    /// <summary> 拠点の耐久値 </summary>
    int m_baseHP = 20;
    [SerializeField] GameObject m_slider;

    void Start()
    {
        m_slider.GetComponent<Slider>();
        m_slider.SetActive(true);
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
