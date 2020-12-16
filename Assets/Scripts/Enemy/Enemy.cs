using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //敵のステータス
    int attack;
    int hp;
    float coolTime;

    public void SetEnemyData(int attack, int hp, float coolTime)
    {
        this.attack = attack;
        Debug.Log("攻撃力は" + attack);
        this.hp = hp;
        this.coolTime = coolTime;
    }

    /// <summary> 拠点を攻撃する </summary>
    public void Attack()
    {
        Debug.Log("拠点が攻撃された" + attack);
    }
}
