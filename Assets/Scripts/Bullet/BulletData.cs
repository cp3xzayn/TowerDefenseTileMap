using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    /// <summary>弾のダメージ </summary>
    [SerializeField]int m_bulDamage = 1;
    /// <summary>射程範囲</summary>
    [SerializeField]float m_limitRange = 5f;

    Bullet bullet;

    void Start()
    {
        bullet = GetComponent<Bullet>();
        //弾のステータスをセットする
        bullet.SetBullet(m_bulDamage, m_limitRange);
    }
}
