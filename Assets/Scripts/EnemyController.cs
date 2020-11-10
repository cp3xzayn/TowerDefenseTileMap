﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject m_mapGene;
    GameObject m_eneGene;
    //敵の進むスピード
    [SerializeField] float m_move = 0.2f;
    SpriteRenderer m_sprite;
    Animator m_anim;
    Rigidbody2D m_rb;
    //敵のポジション
    Vector3 m_enePos;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_mapGene = GameObject.Find("MapGenerator");
        m_eneGene = GameObject.Find("EnemyGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        m_enePos = this.transform.position;
        HowToMove(m_enePos.x, m_enePos.y, m_enePos);
    }

    public void HowToMove(float fx, float fy, Vector3 enePos)
    {
        int x = (int)Mathf.Floor(fx);
        int y = (int)Mathf.Floor(fy);
        //Mapの情報を取得する
        Test t = m_mapGene.GetComponent<Test>();
        int[,] m = t.Map;
        int nextX;
        int nextY;
        //動きたい場所の位置情報
        Vector3 nextPosX = new Vector3Int(x + 1, y, 0);
        Vector3 nextPosY = new Vector3Int(x, y + 1, 0);

        //敵が生成ポジにいるとき
        if (m[x, y] == 0)
        {
            nextX = m[x + 1, y];
            nextY = m[x, y + 1];
            //x方向に道があるとき、移動する
            if (nextX == 1)
            {
                m_rb.velocity = new Vector3(m_move, 0, 0);
            }
        }
        //敵が道にいるとき
        if (m[x, y] == 1)
        {
            nextX = m[x + 1, y];
            nextY = m[x, y + 1];
            //x方向に道があるとき、移動する
            if (nextX == 1)
            {
                m_rb.velocity = new Vector3(m_move, 0, 0);
            }
            //y方向に道があるとき、移動する
            if (nextY == 1)
            {
                m_rb.velocity = new Vector3(0, m_move, 0);
            }
            //x方向に拠点があるとき、移動する
            if (nextX == 3)
            {
                m_rb.velocity = new Vector3(m_move, 0, 0);
            }
            //y方向に拠点があるとき、移動する
            if (nextY == 3)
            {
                m_rb.velocity = new Vector3(0, m_move, 0);
            }
        }
        //敵が拠点にたどり着いたら行動を止める
        if (m[x, y] == 3)
        {
            m_rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
