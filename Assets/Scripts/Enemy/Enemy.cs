using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>攻撃力 </summary>
    private int attack;
    /// <summary>HP </summary>
    private int hp;
    /// <summary>拠点を攻撃する頻度 </summary>
    private float coolTime;
    float m_time = 0;
    GameObject m_baseHPObj;
    BaseHpManager m_base;

    void Start()
    {
        m_baseHPObj = GameObject.Find("BaseHPManager");
        m_base = m_baseHPObj.GetComponent<BaseHpManager>();
    }

    /// <summary>
    /// 敵のデータをセットする関数
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="hp"></param>
    /// <param name="coolTime"></param>
    public void SetEnemyData(int attack, int hp, float coolTime)
    {
        this.attack = attack;
        this.hp = hp;
        this.coolTime = coolTime;
    }

    /// <summary> 拠点を攻撃する </summary>
    public void Attack()
    {
        //CoolTime経過したら敵が攻撃する
        m_time += Time.deltaTime;
        if (m_time > coolTime)
        {
            Debug.Log("攻撃" + attack);
            m_base.DecreaseHP(attack);
            m_time = 0f;
        }
    }
    public void HP()
    {

    }
}
