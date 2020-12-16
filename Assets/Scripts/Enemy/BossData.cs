using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossData : MonoBehaviour
{
    /// <summary>Bossが攻撃する間隔</summary>
    float m_eneCoolTime = 5.0f;
    float m_time = 0f;
    /// <summary>Bossの攻撃力 </summary>
    int m_eneAttack = 1;
    /// <summary>BossのHP </summary>
    int m_eneHP = 1;

    /// <summary>Bossのステータスを入れる関数</summary>
    void BossStates()
    {
        Enemy boss;
        boss = this.gameObject.GetComponent<Enemy>();
        boss.SetEnemyData(m_eneAttack, m_eneHP, m_eneCoolTime);
    }

    void Start()
    {
        BossStates();
    }
}
