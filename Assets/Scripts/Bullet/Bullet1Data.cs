using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Data : MonoBehaviour
{
    /// <summary>弾のダメージ </summary>
    [SerializeField]int m_bulDamage = 3;
    /// <summary>射程範囲</summary>
    [SerializeField]float m_limitRange = 5f;

    Bullet bullet;
    Enemy[] enemy;

    void Start()
    {
        bullet = GetComponent<Bullet>();
        //弾のステータスをセットする
        bullet.SetBullet(m_bulDamage);
        bullet.OnshotToEnemy(m_limitRange);
    }
    //敵と当たったら弾を破壊する
    void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = FindObjectsOfType<Enemy>();
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            foreach (var item in enemy)
            {
                item.SetBulletDamage(m_bulDamage);
            }
        }
    }
}
