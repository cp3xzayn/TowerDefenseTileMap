using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
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

    public int MapWidth { get { return m_mapWidth; } }

    //mapを配列で定義
    int[,] map = new int[13, 13]{
        {0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 0},
        {1, 1, 1, 2, 4, 2, 4, 2, 2, 2, 1, 2, 2},
        {2, 2, 1, 2, 4, 2, 2, 4, 2, 1, 1, 2, 4},
        {2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2},
        {2, 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 4, 2},
        {2, 2, 2, 2, 2, 3, 5, 3, 4, 2, 2, 2, 2},
        {2, 2, 4, 2, 2, 5, 6, 5, 2, 2, 4, 2, 4},
        {2, 2, 2, 2, 4, 3, 5, 3, 2, 4, 2, 2, 2},
        {2, 4, 2, 2, 1, 1, 2, 1, 2, 2, 2, 4, 2},
        {2, 2, 2, 1, 1, 2, 2, 1, 2, 4, 2, 2, 2},
        {2, 2, 2, 1, 4, 2, 4, 1, 2, 2, 2, 2, 2},
        {2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 4, 2},
        {0, 1, 4, 2, 2, 4, 2, 2, 2, 4, 1, 1, 0}
    };
    //プロパティ
    public int[,] Map { get { return map; } }

    // Start is called before the first frame update
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
        TestMap(m_startTile[0], m_enemyRoadTile[0], m_prayerRoadTile[0], m_hubTile[0], m_wallTile[0], m_hubRoadTile[0], m_vector3Int);
    }

    void TestMap(Tile es, Tile er, Tile pr, Tile hu, Tile ob, Tile hr, Vector3Int position)
    {
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
