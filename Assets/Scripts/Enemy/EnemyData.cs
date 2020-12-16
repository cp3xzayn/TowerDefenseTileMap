﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    /// <summary>敵が攻撃する間隔</summary>
    float m_eneCoolTime = 5.0f;
    float m_time = 0f;
    /// <summary>敵の攻撃力 </summary>
    int m_eneAttack = 1;
    /// <summary>敵のHP </summary>
    int m_eneHP = 1;

    /// <summary>敵のステータスを入れる関数</summary>
    void EnemyStates()
    {
        Enemy eneParty;
        eneParty = this.gameObject.GetComponent<Enemy>();
        eneParty.SetEnemyData(m_eneAttack, m_eneHP, m_eneCoolTime);
    }

    void Start()
    {
        EnemyStates();
    }
}
