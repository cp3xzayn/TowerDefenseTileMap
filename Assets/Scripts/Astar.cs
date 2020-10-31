using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Astar : MonoBehaviour
{
    [SerializeField] Tilemap m_tilemap;
    /// <summary>部屋のxの長さ</summary>
    [SerializeField] int m_roomX = 5;
    /// <summary>部屋のyの長さ</summary>
    [SerializeField] int m_roomY = 5;
    /// <summary>壁のタイル</summary>
    Tile[] m_wallTile;
    /// <summary>道のタイル</summary>
    Tile[] m_roadTile;
    /// <summary>スタートのタイル/ </summary>
    Tile[] m_startTile;
    /// <summary>スタートのタイル/ </summary>
    Tile[] m_goalTile;

    //それぞれのTileの状態
    enum NodState
    {
        None,
        Open,
        Close
    }
    NodState nodState;
    /// <summary>実コスト/ </summary>
    int cost;
    /// <summary>推定コスト/ </summary>
    int estimatedCost;


    // Start is called before the first frame update
    void Start()
    {
        //Resourcesフォルダーからタイルを読み込む
        m_roadTile = Resources.LoadAll<Tile>("RoadPalette");
        m_wallTile = Resources.LoadAll<Tile>("WallPalette");
        m_startTile = Resources.LoadAll<Tile>("StartPalette");
        m_goalTile = Resources.LoadAll<Tile>("GoalPalette");
        Vector3Int m_vector3Int = new Vector3Int(0, 0, 0);
        RoomMaping(m_wallTile[0], m_roadTile[0], m_startTile[0], m_goalTile[0], m_vector3Int, m_roomX, m_roomY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RoomMaping(Tile wallTile, Tile roadTile, Tile startTile, Tile goalTile, Vector3Int position, int roomX, int roomY)
    {
        //roomX×roomYの部屋を想定(xの横並びで考える)
        Tile[,] tile = new Tile[roomX, roomY];
        //置くtileを配列に収納
        for (int i = 0; i < roomX; i++)
        {
            for (int j = 0; j < roomY; j++)
            {
                tile[i, j] = roadTile;
                position = new Vector3Int(i, j, 0);
                m_tilemap.SetTile(position, tile[i, j]);
            }
        }
        //スタートゴール、障害物を配列に格納する
        tile[0, 0] = startTile;
        tile[3, 0] = wallTile;
        tile[4, 0] = wallTile;
        tile[4, 1] = wallTile;
        tile[1, 2] = wallTile;
        tile[3, 2] = wallTile;
        tile[2, 3] = wallTile;
        tile[0, 4] = wallTile;
        tile[4, 4] = goalTile;

        //スタート、ゴール、障害物をマップに配置
        m_tilemap.SetTile(new Vector3Int(0, 0, 0), tile[0, 0]);
        m_tilemap.SetTile(new Vector3Int(3, 0, 0), tile[3, 0]);
        m_tilemap.SetTile(new Vector3Int(4, 0, 0), tile[4, 0]);
        m_tilemap.SetTile(new Vector3Int(4, 1, 0), tile[4, 1]);
        m_tilemap.SetTile(new Vector3Int(1, 2, 0), tile[1, 2]);
        m_tilemap.SetTile(new Vector3Int(3, 2, 0), tile[3, 2]);
        m_tilemap.SetTile(new Vector3Int(2, 3, 0), tile[2, 3]);
        m_tilemap.SetTile(new Vector3Int(0, 4, 0), tile[0, 4]);
        m_tilemap.SetTile(new Vector3Int(4, 4, 0), tile[4, 4]);

    }
}
