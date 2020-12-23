using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoMap : MonoBehaviour
{
    [SerializeField] Tilemap m_tilemap;
    /// <summary>敵のスタート位置のタイル/ </summary>
    Tile[] m_startTile;
    /// <summary>敵の歩く道のタイル</summary>
    Tile[] m_enemyRoadTile;
    /// <summary>Playerの歩く道のタイル </summary>
    Tile[] m_prayerRoadTile;
    /// <summary>拠点のタイル </summary>
    Tile[] m_hubTile;
    /// <summary>障害物のタイル</summary>
    Tile[] m_wallTile;
    /// <summary> Playerが歩ける拠点のタイル</summary>
    Tile[] m_hubRoadTile;
    /// <summary>mapの幅</summary>
    [SerializeField] int m_mapWidth = 13;
    /// <summary>mapの配列</summary>
    int[,] map;

    void Start()
    {
        //Resourcesフォルダーからタイルを読み込む
        m_startTile = Resources.LoadAll<Tile>("StartPalette");
        m_enemyRoadTile = Resources.LoadAll<Tile>("EnemyRoadPalette");
        m_prayerRoadTile = Resources.LoadAll<Tile>("PlayerRoadPalette");
        m_hubTile = Resources.LoadAll<Tile>("HubPalette");
        m_wallTile = Resources.LoadAll<Tile>("WallPalette");
        m_hubRoadTile = Resources.LoadAll<Tile>("HubRoadPalette");
        Vector3Int m_vector3Int = new Vector3Int(0, 0, 0);
        AutoMapGene(m_startTile[0], m_enemyRoadTile[0], m_prayerRoadTile[0], m_hubTile[0], m_wallTile[0], m_hubRoadTile[0], m_vector3Int);
    }

    /// <summary>
    /// mapの情報をセットする関数
    /// </summary>
    void SetMapInfo()
    {
        //mapの幅を設定する
        map = new int[m_mapWidth, m_mapWidth];
        //一度全てPlayerRoadを配列に入れる
        for (int i = 0; i < m_mapWidth; i++)
        {
            for (int j = 0; j < m_mapWidth; j++)
            {
                map[i, j] = 2;
            }
        }
        //敵生成ポジションを決める
        map[0, 0] = 0;
        map[0, m_mapWidth - 1] = 0;
        map[m_mapWidth - 1, 0] = 0;
        map[m_mapWidth - 1, m_mapWidth - 1] = 0;
    }

    /// <summary>
    /// mapを生成する関数
    /// </summary>
    void AutoMapGene(Tile es, Tile er, Tile pr, Tile hu, Tile ob, Tile hr, Vector3Int position)
    {
        SetMapInfo();
        //mapの配列にTileを配置
        for (int i = 0; i < m_mapWidth; i++)
        {
            for (int j = 0; j < m_mapWidth; j++)
            {
                position = new Vector3Int(i, j, 0);
                switch (map[i, j])
                {
                    case 0:
                        m_tilemap.SetTile(position, es);
                        break;
                    case 1:
                        m_tilemap.SetTile(position, er);
                        break;
                    case 2:
                        m_tilemap.SetTile(position, pr);
                        break;
                    case 3:
                        m_tilemap.SetTile(position, hu);
                        break;
                    case 4:
                        m_tilemap.SetTile(position, ob);
                        break;
                    case 5:
                        m_tilemap.SetTile(position, hr);
                        break;
                    case 6:
                        m_tilemap.SetTile(position, hr);
                        break;
                }
            }
        }
    }
}
