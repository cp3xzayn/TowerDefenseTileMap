using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary>敵のオブジェクト </summary>
    [SerializeField] GameObject m_enemy;
    GameObject mapGene;

    void Start()
    {
        mapGene = GameObject.Find("MapGenerator");
        //Mapを検索し、敵を生成する
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                EneGene(i, j);
            }
        }
    }

    void Update()
    {
        
    }

    void EneGene(int x, int y)
    {
        //Mapの情報を取得する
        Test t = mapGene.GetComponent<Test>();
        int[,] m = t.Map;
        Vector3Int enePosition = new Vector3Int(x, y, 0);
        //TileがEnemyStartの時生成する
        if (m[x, y] == 0)
        {
            Instantiate(m_enemy, enePosition, Quaternion.identity);
        }
    }
}
