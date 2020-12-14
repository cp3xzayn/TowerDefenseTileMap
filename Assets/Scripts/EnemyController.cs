﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject m_mapGene;
    GameObject m_eneGene;
    /// <summary>敵の進むスピード </summary>
    [SerializeField] float m_move = 1.0f;
    /// <summary>敵が攻撃する間隔</summary>
    [SerializeField] float m_eneAttackTime = 5.0f;
    float m_time = 0f;
    /// <summary>敵の攻撃力 </summary>
    public int m_eneAttack = 1;
    /// <summary>敵が攻撃したかを判定する変数</summary>
    public int isAttack = 0;
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
        //Debug.Log(isAttack);
        if (isAttack == 1)
        {
            isAttack = 0;
        }
    }
    //敵が移動する関数
    public void HowToMove(float fx, float fy, Vector3 enePos)
    {
        //取得した座標を整数に戻す(切り下げ)
        int x = (int)Mathf.Floor(fx);
        int y = (int)Mathf.Floor(fy);
        //取得した座標を整数に戻す(切り上げ)
        int x2 = (int)Mathf.Ceil(fx);
        int y2 = (int)Mathf.Ceil(fy);
        //Mapの情報を取得する
        MapGenerator t = m_mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        //進んだ先の配列の情報を取得するための変数
        int nextX;
        int nextY;
        //敵が左下から沸いたとき
        if (x < 6 && y < 6)
        {
            //敵がいる場所に応じて処理を変える
            switch (m[x, y])
            {
                //敵が生成場所にいるとき
                case 0:
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
                    break;
                //敵が道にいるとき
                case 1:
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
                    break;
                //敵が拠点にいるとき
                case 3:
                    m_rb.velocity = new Vector3(0, 0, 0);
                    EneAttack();
                    break;
            }
        }
        //敵が左上から沸いたとき
        if (x < 6 && y2 > 6)
        {
            //敵がいる場所に応じて処理を変える
            switch (m[x, y2])
            {
                //敵が生成場所にいるとき
                case 0:
                    nextX = m[x + 1, y2];
                    nextY = m[x, y2 - 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    break;
                //敵が道にいるとき
                case 1:
                    nextX = m[x + 1, y2];
                    nextY = m[x, y2 - 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    //x方向に拠点があるとき、移動する
                    if (nextX == 3)
                    {
                        m_rb.velocity = new Vector3(m_move, 0, 0);
                    }
                    //y方向に拠点があるとき、移動する
                    if (nextY == 3)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    break;
                //敵が拠点にいるとき
                case 3:
                    m_rb.velocity = new Vector3(0, 0, 0);
                    EneAttack();
                    break;
            }
        }
        //敵が右下から沸いたとき
        if (x2 > 6 && y < 6)
        {
            //敵がいる場所に応じて処理を変える
            switch (m[x2, y])
            {
                //敵が生成場所にいるとき
                case 0:
                    nextX = m[x2 - 1, y];
                    nextY = m[x2, y + 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, m_move, 0);
                    }
                    break;
                //敵が道にいるとき
                case 1:
                    nextX = m[x2 - 1, y];
                    nextY = m[x2, y + 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, m_move, 0);
                    }
                    //x方向に拠点があるとき、移動する
                    if (nextX == 3)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に拠点があるとき、移動する
                    if (nextY == 3)
                    {
                        m_rb.velocity = new Vector3(0, m_move, 0);
                    }
                    break;
                //敵が拠点にいるとき
                case 3:
                    m_rb.velocity = new Vector3(0, 0, 0);
                    EneAttack();
                    break;
            }
        }
        //敵が右上から沸いたとき
        if (x2 > 6 && y2 > 6)
        {
            //敵がいる場所に応じて処理を変える
            switch (m[x2, y2])
            {
                //敵が生成場所にいるとき
                case 0:
                    nextX = m[x2 - 1, y2];
                    nextY = m[x2, y2 - 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    break;
                //敵が道にいるとき
                case 1:
                    nextX = m[x2 - 1, y2];
                    nextY = m[x2, y2 - 1];
                    //x方向に道があるとき、移動する
                    if (nextX == 1)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に道があるとき、移動する
                    if (nextY == 1)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    //x方向に拠点があるとき、移動する
                    if (nextX == 3)
                    {
                        m_rb.velocity = new Vector3(-m_move, 0, 0);
                    }
                    //y方向に拠点があるとき、移動する
                    if (nextY == 3)
                    {
                        m_rb.velocity = new Vector3(0, -m_move, 0);
                    }
                    break;
                //敵が拠点にいるとき
                case 3:
                    m_rb.velocity = new Vector3(0, 0, 0);
                    EneAttack();
                    break;
            }
        }
    }

    //敵の攻撃に関する関数
    public void EneAttack()
    {
        m_time += Time.deltaTime;
        if (m_time > m_eneAttackTime)
        {
            isAttack = 1;
            Debug.Log("攻撃");
            m_time = 0f;
        }
    }

    //弾が当たったとき敵を破壊する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
