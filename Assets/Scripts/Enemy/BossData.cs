using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossData : MonoBehaviour
{
    /// <summary>Bossが攻撃する間隔</summary>
    [SerializeField] float m_bossCoolTime = 4.0f;
    float m_time = 0f;
    /// <summary>Bossの攻撃力 </summary>
    [SerializeField] int m_bossAttack = 3;
    /// <summary>BossのHP </summary>
    [SerializeField] int m_bossHP = 3;

    /// <summary>Bossのステータスを入れる関数</summary>
    void BossStates()
    {
        Enemy boss;
        boss = this.gameObject.GetComponent<Enemy>();
        boss.SetEnemyData(m_bossAttack, ref m_bossHP, m_bossCoolTime);
    }

    void Start()
    {
        BossStates();
    }
}
