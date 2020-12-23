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
    /// <summary>弾のダメージ </summary>
    int bulletDamage;
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
    public void SetEnemyData(int attack, ref int eHP, float coolTime)
    {
        this.attack = attack;
        this.coolTime = coolTime;
        hp = eHP;
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

    /// <summary>
    /// 弾のダメージをセットし、敵のHPを減らす関数
    /// </summary>
    /// <param name="bDamage"></param>
    public void SetBulletDamage(int bDamage)
    {
        bulletDamage = bDamage;
        hp -= bulletDamage;
        //敵のHPが0以下になったら
        if (hp <= 0)
        {
            //敵を破壊する
            Destroy(this.gameObject);
            //弾が当たったときフィールドにいる敵全員のHPが減る問題が発生
        }
    }
}
