using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
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

    //mapを配列で定義
    int[,] map = new int[13, 13]{
        {0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2},
        {1, 1, 1, 2, 4, 2, 4, 2, 2, 2, 1, 2, 2},
        {2, 2, 1, 2, 4, 2, 2, 4, 2, 1, 1, 2, 4},
        {2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2},
        {2, 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 4, 2},
        {2, 2, 2, 2, 2, 3, 3, 3, 4, 2, 2, 2, 2},
        {2, 2, 4, 2, 2, 3, 3, 3, 2, 2, 4, 2, 4},
        {2, 2, 2, 2, 4, 3, 3, 3, 2, 4, 2, 2, 2},
        {2, 4, 2, 2, 1, 1, 2, 1, 2, 2, 2, 4, 2},
        {2, 2, 2, 1, 1, 2, 2, 1, 2, 4, 2, 2, 2},
        {2, 2, 2, 1, 4, 2, 4, 1, 2, 2, 2, 2, 2},
        {2, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 4, 2},
        {2, 1, 4, 2, 2, 4, 2, 2, 2, 4, 1, 1, 2}
    };
    //プロパティ
    public int[,] Map
    {
        get{ return map;}//取得用
    }

    // Start is called before the first frame update
    void Start()
    {
        //Resourcesフォルダーからタイルを読み込む
        m_startTile = Resources.LoadAll<Tile>("StartPalette");
        m_enemyRoadTile = Resources.LoadAll<Tile>("EnemyRoadPalette");
        m_prayerRoadTile = Resources.LoadAll<Tile>("PlayerRoadPalette");
        m_hubTile = Resources.LoadAll<Tile>("HubPalette");
        m_wallTile = Resources.LoadAll<Tile>("WallPalette");
        Vector3Int m_vector3Int = new Vector3Int(0, 0, 0);
        TestMap(m_startTile[0], m_enemyRoadTile[0], m_prayerRoadTile[0], m_hubTile[0], m_wallTile[0], m_vector3Int);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TestMap(Tile es ,Tile er, Tile pr, Tile hu, Tile ob, Vector3Int position)
    {
        //mapの配列にTileを配置
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
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
                }
            }
        }
    }
}

