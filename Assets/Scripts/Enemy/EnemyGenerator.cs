using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputJson
{
    public int[] m_enemy;
}


public class EnemyGenerator : MonoBehaviour
{
    /// <summary>敵のオブジェクト </summary>
    GameObject m_enemy;
    /// <summary>ボスのオブジェクト</summary>
    GameObject m_boss;
    GameObject mapGene;
    /// <summary>敵の生成ポジション </summary>
    Vector3Int enePosition;
    /// <summary>Bossの生成ポジション </summary>
    Vector3Int bossPosition;

    int m_index;
    int m_eneIndex;
    int m_loadEneLength;

    void Start()
    {
        m_enemy = Resources.Load<GameObject>("Enemy");
        m_boss = Resources.Load<GameObject>("Boss");
        mapGene = GameObject.Find("MapGenerator");
        GetLength();
    }
    /// <summary>
    /// Jsonファイルから敵生成の配列を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadEneGene(int index)
    {
        //敵を生成したらIndexを一つ進める処理を書く
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_eneIndex = inputJson.m_enemy[m_index];
        return m_eneIndex;
    }

    /// <summary>
    /// 取得したJsonファイルの配列の長さを取得する
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_loadEneLength = inputJson.m_enemy.Length;
        return m_loadEneLength;
    }

    //敵を生成する
    public void OnEneGene()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                EneGene(i, j);
            }
        }
    }

    /// <summary>
    /// 敵を生成する関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void EneGene(int x, int y)
    {
        //Mapの情報を取得する
        MapGenerator t = mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        enePosition = new Vector3Int(x, y, 0);
        //TileがEnemyStartの時生成する
        switch (m_eneIndex)
        {
            case 0:
                if (m[x, y] == 0)
                {
                    Instantiate(m_enemy, enePosition, Quaternion.identity);
                }
                break;
            case 1:
                if (m[x, y] == 0)
                {
                    Instantiate(m_boss, bossPosition, Quaternion.identity);
                }
                break;
        }
    }
}
