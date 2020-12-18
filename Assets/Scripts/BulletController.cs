using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // スピード
    [SerializeField]float m_speed = 1.0f;
    private GameObject m_weapon;
    private GameObject[] m_enemy;
    /// <summary>弾のダメージ </summary>
    int m_bulDamage = 1;
    /// <summary>弾の生成ポジション</summary>
    private Vector3 m_startPosition;
    /// <summary>敵のポジション</summary>
    private Vector3[] m_goalPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;
    /// <summary>射程範囲</summary>
    [SerializeField] float m_limitRange = 3.0f;

    private float m_destroyTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //二点間の距離を代入
        //m_distance = Vector2.Distance(m_startPosition, m_goalPosition);
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        m_goalPosition = new Vector3[m_enemy.Length];
        OnShot();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の位置
        //float nowLocation = (Time.time * m_speed) / m_distance;
        //オブジェクトの移動
        //this.transform.position = Vector2.Lerp(m_startPosition, m_goalPosition, nowLocation);
        //弾を時間で破壊する
        m_destroyTime -= Time.deltaTime;
        if (m_destroyTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnShot()
    {
        //フィールド上にいる敵を配列に格納
        for (int i = 0; i < m_enemy.Length; i++)
        {
            m_goalPosition[i] = m_enemy[i].transform.position;
            // 二点間の距離を代入
            m_distance = Vector3.Distance(m_startPosition, m_goalPosition[i]);
            //敵と兵器の距離が範囲内だったら
            if (m_distance < m_limitRange)
            {
                Debug.Log("敵検知、弾発射");
                this.transform.position = m_goalPosition[i];
            }
        }
    }

    //敵と当たったら弾を破壊する
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Debug.Log("敵に当たった、弾破壊");
        }
    }
}
