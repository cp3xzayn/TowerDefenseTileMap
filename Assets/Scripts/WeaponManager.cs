using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]GameObject m_bullet;
    private GameObject m_enemy;
    /// <summary>弾の生成ポジション</summary>
    private Vector3 m_startPosition;
    /// <summary>敵のポジション</summary>
    private Vector3 m_goalPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;
    [SerializeField] float m_limitRange = 4.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnShot()
    {
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectWithTag("Enemy");
        m_goalPosition = m_enemy.transform.position;
        //二点間の距離を代入
        m_distance = Vector2.Distance(m_startPosition, m_goalPosition);
        Debug.Log(m_distance);
        if (m_distance <= m_limitRange)
        {
            Instantiate(m_bullet, this.transform.position, Quaternion.identity);
        }
        
    }
}
