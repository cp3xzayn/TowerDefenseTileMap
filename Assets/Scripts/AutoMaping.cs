﻿using UnityEngine;
using UnityEngine.Tilemaps;
public class AutoMaping : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        //Resourcesフォルダーからタイルを読み込む
        m_roadTile = Resources.LoadAll<Tile>("RoadPalette");
        m_wallTile = Resources.LoadAll<Tile>("WallPalette");
        Vector3Int m_vector3Int = new Vector3Int(0, 0, 0);
        RoomMaping(m_wallTile[0], m_roadTile[0], m_vector3Int, m_roomX, m_roomY);
    }
    // Update is called once per frame
    void Update()
    {
    }
    void RoomMaping(Tile wallTile, Tile roadTile, Vector3Int position, int roomX, int roomY)
    {
        int tileIndex = roomX * roomY;
        //roomX×roomYの部屋を想定(xの横並びで考える)
        Tile[] tile = new Tile[tileIndex];
        //置くtileを配列に収納
        for (int i = 0; i < tile.Length; i++)
        {
            tile[i] = wallTile;
        }


        int j = 0;
        tile[j] = roadTile;
        
        while (j < tileIndex)
        {
            int xory = Random.Range(0, 2);
            if (xory == 0)
            {
                j++;
                if (j < tileIndex)
                {
                    tile[j] = roadTile;
                }
            }
            if (xory == 1)
            {
                j += roomY;
                if (j < tileIndex)
                {
                    tile[j] = roadTile;
                }
            }
        }
        

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
    }
}