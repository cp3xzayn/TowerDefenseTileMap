using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        m_enemy = Resources.Load<GameObject>("Enemy");
        m_boss = Resources.Load<GameObject>("Boss");
        mapGene = GameObject.Find("MapGenerator");
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
    //Bossを生成する
    public void OnBossGene()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                BossGene(i, j);
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
        if (m[x, y] == 0)
        {
            Instantiate(m_enemy, enePosition, Quaternion.identity);
        }
    }
    /// <summary>
    /// Bossを生成する関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void BossGene(int x, int y)
    {
        //Mapの情報を取得する
        MapGenerator t = mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        bossPosition = new Vector3Int(x, y, 0);
        //TileがEnemyStartの時生成する
        if (m[x, y] == 0)
        {
            Instantiate(m_boss, bossPosition, Quaternion.identity);
        }
    }
}
