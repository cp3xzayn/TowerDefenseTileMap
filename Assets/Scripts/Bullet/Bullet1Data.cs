using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Data : MonoBehaviour
{
    /// <summary>弾のダメージ </summary>
    int m_bulDamage = 3;
    /// <summary>射程範囲</summary>
    float m_limitRange = 3f;

    Bullet bullet;

    void Start()
    {
        bullet = GetComponent<Bullet>();
        //弾のステータスをセットする
        bullet.SetBullet(m_bulDamage);
        bullet.OnshotToEnemy(m_limitRange);
    }
}
