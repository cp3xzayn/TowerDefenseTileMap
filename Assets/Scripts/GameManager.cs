using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject m_mapGene;
    [SerializeField] GameObject m_player;
    /// <summary>プレイヤーの生成ポジション </summary>
    Vector3Int plaPosition;

    enum GameState
    {
        Preparation,
        Battle,
        Finish
    }

    // Start is called before the first frame update
    void Start()
    {
        m_mapGene = GameObject.Find("MapGenerator");
        //playerを生成する
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                PlayerInstance(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //playerを生成する関数
    void PlayerInstance(int x, int y)
    {
        //Mapの情報を取得する
        MapGenerator t = m_mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        plaPosition = new Vector3Int(x, y, 0);
        if (m[x, y] == 6)
        {
            Instantiate(m_player, plaPosition, Quaternion.identity);
        }
    }
}
