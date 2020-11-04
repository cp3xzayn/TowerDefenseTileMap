using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] Tilemap m_tilemap;
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
        TestMap(m_wallTile[0], m_roadTile[0], m_startTile[0], m_goalTile[0], m_vector3Int);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TestMap(Tile w, Tile r, Tile s, Tile g, Vector3Int position)
    {
        //mapを配列で定義
        int[,] map = new int[5, 5]{
            { 0, 2, 2, 2, 3},
            { 2, 2, 3, 2, 2},
            { 2, 2, 2, 3, 2},
            { 3, 2, 3, 2, 2},
            { 3, 3, 2, 2, 1}
        };
        //mapの配列にTileを配置
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                position = new Vector3Int(i, j, 0);
                switch (map[i, j])
                {
                    case 0:
                        m_tilemap.SetTile(position, s);
                        break;
                    case 1:
                        m_tilemap.SetTile(position, g);
                        break;
                    case 2:
                        m_tilemap.SetTile(position, r);
                        break;
                    case 3:
                        m_tilemap.SetTile(position, w);
                        break;
                }
            }
        }

    }
}
