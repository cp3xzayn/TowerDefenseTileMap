using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //弾のステータス
    int bulletDamage = 1;
    /// <summary>射程範囲</summary>
    [SerializeField] float m_limitRange = 5f;

    /// <summary>
    /// 弾のダメージをセットする関数
    /// </summary>
    /// <param name="bDamage"></param>
    public void SetBullet(int bDamage)
    {
        bulletDamage = bDamage;
    }

    public int Damage
    {
        set { bulletDamage = value; }
        get { return bulletDamage; }
    }

    //[SerializeField]float m_speed = 1.0f;
    private GameObject[] m_enemy;
    /// <summary>弾の生成ポジション</summary>
    private Vector3 m_startPosition;
    /// <summary>敵のポジション</summary>
    private Vector3[] m_goalPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;
    /// <summary>弾を破壊するまでの時間 </summary>
    private float m_destroyTime = 0.1f;


    void Start()
    {
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        m_goalPosition = new Vector3[m_enemy.Length];
        //弾のダメージを初期化する
        /*GameObject weapon = GameObject.Find("Weapon(Clone)");
        WeaponManager wm = weapon.GetComponent<WeaponManager>();
        int index = wm.WeaponIndex;
        m_bulDamage = wm.LoadBulletDamage(index);
        SetBullet(m_bulDamage);*/
        Debug.Log("弾のダメージは" + bulletDamage);
        OnshotToEnemy(m_limitRange);
    }
    void Update()
    {
        //弾を時間で破壊する
        m_destroyTime -= Time.deltaTime;
        if (m_destroyTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 弾を敵に飛ばす関数
    /// </summary>
    public void OnshotToEnemy(float bRange)
    {
        //弾の射程範囲をセット
        float bulletRange = bRange;
        //フィールド上にいる敵を配列に格納
        for (int i = 0; i < m_enemy.Length; i++)
        {
            m_goalPosition[i] = m_enemy[i].transform.position;
            // 二点間の距離を代入
            m_distance = Vector3.Distance(m_startPosition, m_goalPosition[i]);
            //敵と兵器の距離が範囲内だったら
            if (m_distance < bulletRange)
            {
                Debug.Log("敵検知、弾発射");
                this.transform.position = m_goalPosition[i];
            }
        }
    }
    

    //敵と当たったら弾を破壊する
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        //オブジェクトがEnemyだった場合
        if (ob.tag == ("Enemy"))
        {
            Enemy enemy = ob.GetComponent<Enemy>();
            enemy.SetBulletDamage(bulletDamage);
        }
    }
}
