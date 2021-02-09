using UnityEngine;

public class BulletData : MonoBehaviour
{
    /// <summary>弾のダメージ </summary>
    [SerializeField] int m_bulDamage = 1;
    /// <summary>射程範囲</summary>
    [SerializeField] float m_limitRange = 5f;

    public int BulletDamage { get { return m_bulDamage; } }
    public float LimitRange { get { return m_limitRange; } }

    Bullet bullet;

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
        GameObject ob = collision.gameObject;
        //オブジェクトがEnemyだった場合
        if (ob.tag == ("Enemy"))
        {
            Enemy enemy = ob.GetComponent<Enemy>();
            enemy.SetBulletDamage(m_bulDamage);
        }
    }
}
