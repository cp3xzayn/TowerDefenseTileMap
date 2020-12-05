using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // スピード
    [SerializeField]float m_speed = 1.0f;
    private GameObject m_enemy;
    /// <summary>弾の生成ポジション</summary>
    private Vector3 m_startPosition;
    /// <summary>敵のポジション</summary>
    private Vector3 m_goalPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;

    // Start is called before the first frame update
    void Start()
    {
        //弾と敵のポジションを取得する
        m_startPosition = this.transform.position;
        m_enemy = GameObject.FindGameObjectWithTag("Enemy");
        m_goalPosition = m_enemy.transform.position;
        //二点間の距離を代入
        m_distance = Vector2.Distance(m_startPosition, m_goalPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の位置
        float nowLocation = (Time.time * m_speed) / m_distance;
        //オブジェクトの移動
        this.transform.position = Vector2.Lerp(m_startPosition, m_goalPosition, nowLocation);
    }

    //敵と当たったら弾を破壊する
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Debug.Log("弾破壊");
        }
    }
}
