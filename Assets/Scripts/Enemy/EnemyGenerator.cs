using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputJson
{
    public int[] m_wave;
    public float[] m_waveCoolTime;
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


    void Start()
    {
        m_enemy = Resources.Load<GameObject>("Enemy");
        m_boss = Resources.Load<GameObject>("Boss");
        mapGene = GameObject.Find("MapGenerator");
    }

    /// <summary>
    /// Jsonファイルから敵生成の配列を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadEneGeneWave(int index)
    {
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_eneIndex = inputJson.m_wave[m_index];
        return m_eneIndex;
    }

    /// <summary>
    /// 取得したJsonファイルの配列の長さを取得する
    /// </summary>
    /// <returns></returns>
    public int GetLengthWave()
    {
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_loadEneLength = inputJson.m_wave.Length;
        return m_loadEneLength;
    }


    /// <summary>
    /// 敵を生成する関数Wave1
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="index"></param>
    public void EneGene(int x, int y, int index)
    {
        //Mapの情報を取得する
        MapGenerator t = mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        enePosition = new Vector3Int(x, y, 0);
        //TileがEnemyStartの時生成する
        switch (LoadEneGeneWave(index))
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
}
