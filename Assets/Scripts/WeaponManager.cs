using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject m_bullet;
    private GameObject[] m_enemy;
    /// <summary>弾の生成ポジション</summary>
    private Vector3 m_startPosition;
    /// <summary>敵のポジション</summary>
    private Vector3[] m_goalPosition;
    private Vector3 m_enemyPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;
    /// <summary>射程範囲</summary>
    [SerializeField] float m_limitRange = 4.0f;
    /// <summary>弾の発射間隔</summary>
    [SerializeField] float m_shootTime = 2.0f;
    private float m_time = 0f;

    void Start()
    {

    }

    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time > m_shootTime)
        {
            OnShot();
            m_time = 0f;
        }
    }

    public void OnShot()
    {
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        m_goalPosition = new Vector3[m_enemy.Length];
        //フィールド上にいる敵を配列に格納
        for (int i = 0; i < m_enemy.Length; i++)
        {
            m_goalPosition[i] = m_enemy[i].transform.position;
            // 二点間の距離を代入
            m_distance = Vector3.Distance(m_startPosition, m_goalPosition[i]);
            //敵と兵器の距離が範囲内だったら
            if (m_distance <= m_limitRange)
            {
                Debug.Log(m_distance);
                m_enemyPosition = m_goalPosition[i];
                //弾を生成する
                Debug.Log("弾生成");
                Instantiate(m_bullet, this.transform.position, Quaternion.identity);
            }
        }
    }

    //プロパティ
    public Vector3 EnemyPos
    {
        get { return m_enemyPosition; }
    }
}
 