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
    [SerializeField] float m_limitRange = 4.0f;

    void Start()
    {

    }

    void Update()
    {
        OnShot();
    }

    //ボタンが押されたときに（仮）
    public void OnShot()
    {
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        m_goalPosition = new Vector3[m_enemy.Length];
        for (int i = 0; i < m_enemy.Length; i++)
        {
            m_goalPosition[i] = m_enemy[i].transform.position;
            // 二点間の距離を代入
            m_distance = Vector3.Distance(m_startPosition, m_goalPosition[i]);
            Debug.Log(m_distance);
            if (m_distance <= m_limitRange)
            {
                m_enemyPosition = m_goalPosition[i];
                Instantiate(m_bullet, this.transform.position, Quaternion.identity);
            }
        }

    }

    public Vector3 EnemyPos
    {
        get { return m_enemyPosition; }
    }
}
 