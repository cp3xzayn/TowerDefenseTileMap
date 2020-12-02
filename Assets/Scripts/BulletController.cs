using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //終わりの目印
    [SerializeField] Transform m_endMarker;
    // スピード
    [SerializeField]float m_speed = 1.0f;
    [SerializeField] GameObject m_enemy;
    private Vector3 m_startPosition;
    private Vector3 m_goalPosition;
    /// <summary>二点間の距離/summary>
    private float m_distance;

    // Start is called before the first frame update
    void Start()
    {
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
}
