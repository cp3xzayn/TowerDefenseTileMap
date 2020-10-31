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
        int tileIndex = roomX * roomY;
        //roomX×roomYの部屋を想定(xの横並びで考える)
        Tile[] tile = new Tile[tileIndex];
        //置くtileを配列に収納
        for (int i = 0; i < tile.Length; i++)
        {
            tile[i] = roadTile;
        }

        tile[0] = startTile;
        tile[3] = wallTile;
        tile[4] = wallTile;
        tile[9] = wallTile;
        tile[11] = wallTile;
        tile[13] = wallTile;
        tile[17] = wallTile;
        tile[20] = wallTile;
        tile[24] = goalTile;

        int putIndex = 0;
        //配列からtileを置く(xの横並びで考える)
        for (int y = 0; y < roomY; y++)
        {
            for (int x = 0; x < roomX; x++)
            {
                position = new Vector3Int(x, y, 0);
                m_tilemap.SetTile(position, tile[putIndex]);
                putIndex++;
            }
        }

        m_tilemap.SetTile(new Vector3Int(0, 0, 0), tile[0]);
        m_tilemap.SetTile(new Vector3Int(3, 0, 0), tile[3]);
        m_tilemap.SetTile(new Vector3Int(4, 0, 0), tile[4]);
        m_tilemap.SetTile(new Vector3Int(4, 1, 0), tile[9]);
        m_tilemap.SetTile(new Vector3Int(1, 2, 0), tile[11]);
        m_tilemap.SetTile(new Vector3Int(3, 2, 0), tile[13]);
        m_tilemap.SetTile(new Vector3Int(2, 3, 0), tile[17]);
        m_tilemap.SetTile(new Vector3Int(0, 4, 0), tile[20]);
        m_tilemap.SetTile(new Vector3Int(4, 4, 0), tile[24]);
    }
}
