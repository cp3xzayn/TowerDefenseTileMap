using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject m_mapGene;
    GameObject m_eneGene;
    float m_move = 0.1f;
    /// <summary>移動速度</summary>
    [SerializeField] float m_walkSpeed = 1f;
    SpriteRenderer m_sprite;
    Animator m_anim;
    Rigidbody2D m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_mapGene = GameObject.Find("MapGenerator");
        m_eneGene = GameObject.Find("EnemyGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                HowTOMove(i, j);
            }
        }
    }

    public void HowTOMove(int x, int y)
    {
        //Mapの情報を取得する
        Test t = m_mapGene.GetComponent<Test>();
        int[,] m = t.Map;
        int nextX;
        int nextY;
        //今の位置情報
        Vector3 firstPos = new Vector3(x, y, 0);
        //動きたい場所の位置情報
        Vector3 nextPos;
        
        //敵生成ポジションにいるとき
        if (m[x, y] == 0)
        {
            nextX = m[x + 1, y];
            nextY = m[x, y + 1];
            if (nextX == 1)
            {
                nextPos = new Vector3Int(x + 1, y, 0);
                this.transform.position = nextPos;
                x++;
            }
            if (nextY == 1)
            {
                nextPos = new Vector3Int(x, y + 1, 0);
                this.transform.position = nextPos;
                y++;
            }
        }
    }
}
