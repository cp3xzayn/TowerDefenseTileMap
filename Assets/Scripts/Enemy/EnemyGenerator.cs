using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputJson
{
    public int[] m_enemy;
    public int[] m_enemy1;
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
    /// <summary> 配列の要素番号 </summary>
    int m_index;
    /// <summary> 取得した配列の数値 </summary>
    int m_eneIndex;
    /// <summary> 取得した配列の長さ </summary>
    int m_loadEneLength;

    GameObject gameMana;

    int wave;
    GameManager gameManager;

    void Start()
    {
        m_enemy = Resources.Load<GameObject>("Enemy");
        m_boss = Resources.Load<GameObject>("Boss");
        mapGene = GameObject.Find("MapGenerator");
        gameMana = GameObject.Find("GameManager");
    }

    /// <summary>
    /// Jsonファイルから敵生成の配列(Wave1)を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadEneGeneWave1(int index)
    {
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_eneIndex = inputJson.m_enemy[m_index];
        return m_eneIndex;
    }

    /// <summary>
    /// Jsonファイルから敵生成の配列(Wave2)を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadEneGeneWave2(int index)
    {
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_eneIndex = inputJson.m_enemy1[m_index];
        return m_eneIndex;
    }

    /// <summary>
    /// 取得したJsonファイルの配列の長さ(Wave1)を取得する
    /// </summary>
    /// <returns></returns>
    public int GetLengthWave1()
    {
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_loadEneLength = inputJson.m_enemy.Length;
        return m_loadEneLength;
    }

    /// <summary>
    /// 取得したJsonファイルの配列の長さ(Wave2)を取得する
    /// </summary>
    /// <returns></returns>
    public int GetLengthWave2()
    {
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_loadEneLength = inputJson.m_enemy1.Length;
        return m_loadEneLength;
    }


    /// <summary>
    /// 敵を生成する関数Wave1
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="index"></param>
    public void EneGene1(int x, int y, int index)
    {
        //Mapの情報を取得する
        MapGenerator t = mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        enePosition = new Vector3Int(x, y, 0);
        //TileがEnemyStartの時生成する
        switch (LoadEneGeneWave1(index))
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
                    Instantiate(m_boss,　enePosition, Quaternion.identity);
                }
                break;
        }
    }

    /// <summary>
    /// 敵を生成する関数wave2
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="index"></param>
    public void EneGene2(int x, int y, int index)
    {
        //Mapの情報を取得する
        MapGenerator t = mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        enePosition = new Vector3Int(x, y, 0);
        //敵の配列2の時のスイッチ分
        switch (LoadEneGeneWave2(index))
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
                    Instantiate(m_boss, enePosition, Quaternion.identity);
                }
                break;
        }
    }
}
