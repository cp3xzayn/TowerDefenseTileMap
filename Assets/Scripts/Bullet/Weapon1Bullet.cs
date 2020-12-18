using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Bullet : MonoBehaviour
{
    /// <summary>弾のダメージ </summary>
    int m_bulDamage = 1;
    /// <summary>射程範囲</summary>
    float m_limitRange = 5.0f;

    Bullet bullet;

    void Start()
    {
        bullet = GetComponent<Bullet>();
        //弾のステータスをセットする
        bullet.SetBullet(m_bulDamage, m_limitRange);
    }

}
