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

        //Playerの沸きポジを決定する
        int baseX = Random.Range(3, m_mapWidth - 4);
        int baseY = Random.Range(3, m_mapWidth - 4);
        map[baseX, baseY] = 6;

        //拠点のPlayerRoadを決定する
        map[baseX - 1, baseY] = 5;
        map[baseX, baseY - 1] = 5;
        map[baseX + 1, baseY] = 5;
        map[baseX, baseY + 1] = 5;

        //敵の歩く道を生成する
        LeftDownPath(baseX, baseY);
        LeftUpPath(baseX, baseY);
        RightDownPath(baseX, baseY);
        RightUpPath(baseX, baseY);

        //敵の到達地点を決定する(道生成の後に置く)
        map[baseX - 1, baseY - 1] = 3;
        map[baseX - 1, baseY + 1] = 3;
        map[baseX + 1, baseY - 1] = 3;
        map[baseX + 1, baseY + 1] = 3;

    }

    /// <summary>
    /// 左下の生成ポジから道を伸ばす関数
    /// </summary>
    /// <param name="basex"></param>
    /// <param name="basey"></param>
    void LeftDownPath(int basex, int basey)
    {
        int lDX = 0;
        int lDY = 0;
        //xかy方向どちらに伸ばすかを判定する
        int XorY;
        //左下からの道を生成する
        while (lDX < basex - 1 || lDY < basey - 1)
        {
            XorY = Random.Range(0, 2);
            switch (XorY)
            {
                case 0:
                    if (lDX < basex - 1)
                    {
                        lDX += 1;
                    }
                    break;
                case 1:
                    if (lDY < basey - 1)
                    {
                        lDY += 1;
                    }
                    break;
            }
            map[lDX, lDY] = 1;
        }
    }
    /// <summary>
    /// 左上の生成ポジから道を伸ばす関数
    /// </summary>
    /// <param name="basex"></param>
    /// <param name="basey"></param>
    void LeftUpPath(int basex, int basey)
    {
        int lDX = 0;
        int lUY = m_mapWidth - 1;
        //xかy方向どちらに伸ばすかを判定する
        int XorY;
        //左下からの道を生成する
        while (lDX < basex - 1 || lUY > basey + 1)
        {
            XorY = Random.Range(0, 2);
            switch (XorY)
            {
                case 0:
                    if (lDX < basex - 1)
                    {
                        lDX += 1;
                    }
                    break;
                case 1:
                    if (lUY > basey + 1)
                    {
                        lUY -= 1;
                    }
                    break;
            }
            map[lDX, lUY] = 1;
        }
    }
    /// <summary>
    /// 右下の生成ポジから道を伸ばす関数
    /// </summary>
    /// <param name="basex"></param>
    /// <param name="basey"></param>
    void RightDownPath(int basex, int basey)
    {
        int lUX = m_mapWidth - 1;
        int lDY = 0;
        //xかy方向どちらに伸ばすかを判定する
        int XorY;
        //左下からの道を生成する
        while (lUX > basex + 1 || lDY < basey - 1)
        {
            XorY = Random.Range(0, 2);
            switch (XorY)
            {
                case 0:
                    if (lUX > basex + 1)
                    {
                        lUX -= 1;
                    }
                    break;
                case 1:
                    if (lDY < basey - 1)
                    {
                        lDY += 1;
                    }
                    break;
            }
            map[lUX, lDY] = 1;
        }
    }
    /// <summary>
    /// 右上の生成ポジから道を伸ばす関数
    /// </summary>
    /// <param name="basex"></param>
    /// <param name="basey"></param>
    void RightUpPath(int basex, int basey)
    {
        int lUX = m_mapWidth - 1;
        int lUY = m_mapWidth - 1;
        //xかy方向どちらに伸ばすかを判定する
        int XorY;
        //左下からの道を生成する
        while (lUX > basex + 1 || lUY > basey + 1)
        {
            XorY = Random.Range(0, 2);
            switch (XorY)
            {
                case 0:
                    if (lUX > basex + 1)
                    {
                        lUX -= 1;
                    }
                    break;
                case 1:
                    if (lUY > basey + 1)
                    {
                        lUY -= 1;
                    }
                    break;
            }
            map[lUX, lUY] = 1;
        }
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
