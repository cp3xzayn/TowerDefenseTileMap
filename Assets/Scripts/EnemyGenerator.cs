using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary>敵のオブジェクト </summary>
    [SerializeField] GameObject m_enemy;
    GameObject mapGene;
    /// <summary>敵の生成ポジション </summary>
    Vector3Int enePosition;

    void Start()
    {
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
}
