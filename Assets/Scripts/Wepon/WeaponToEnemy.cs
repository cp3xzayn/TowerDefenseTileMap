using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToEnemy : MonoBehaviour
{
    LineRenderer lineRenderer;
    private GameObject[] m_enemy;
    Vector3 m_startPosition;
    Vector3[] m_goalPosition;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        WriteLineRenderer();
    }

    void WriteLineRenderer()
    {
        m_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        m_goalPosition = new Vector3[m_enemy.Length];
        lineRenderer.SetPosition(0, this.transform.position);

        //弾の射程範囲をセット
        float bulletRange = 10;
        //フィールド上にいる敵を配列に格納
        for (int i = 0; i < m_enemy.Length; i++)
        {
            m_goalPosition[i] = m_enemy[i].transform.position;
            // 二点間の距離を代入
            float m_distance = Vector3.Distance(m_startPosition, m_goalPosition[i]);
            //敵と兵器の距離が範囲内だったら
            if (m_distance < bulletRange)
            {
                lineRenderer.SetPosition(1, m_goalPosition[i]);
            }
        }
    }
}
