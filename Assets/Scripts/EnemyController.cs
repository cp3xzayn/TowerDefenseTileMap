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

    Vector3 m_enePos;


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
        HowToMove(0, 0, m_enePos);
        m_enePos = this.transform.position;
    }

    public void HowToMove(int x, int y, Vector3 enePos)
    {
        //Mapの情報を取得する
        Test t = m_mapGene.GetComponent<Test>();
        int[,] m = t.Map;
        int nextX;
        int nextY;
        //今の位置情報
        Vector3 firstPos = new Vector3(x, y, 0);
        //動きたい場所の位置情報
        Vector3 nextPosX = new Vector3Int(x + 1, y, 0);
        Vector3 nextPosY = new Vector3Int(x, y + 1, 0);

        //敵が生成ポジにいるとき
        if (m[x, y] == 0)
        {
            nextX = m[x + 1, y];
            nextY = m[x, y + 1];
            if (nextX == 1)
            {
                m_rb.velocity = new Vector3(m_move, 0, 0);
            }
        }

        //敵生成ポジション、道にいるとき
        /*if (m[x, y] == 0 || m[x, y] == 1)
        {
            nextX = m[x + 1, y];
            nextY = m[x, y + 1];
            if (nextX == 1)
            {
                nextPos = new Vector3Int(x + 1, y, 0);
                Debug.Log("x:1, y:0 移動");
                this.transform.position = nextPos;
                x++;
            }
            if (nextY == 1)
            {
                nextPos = new Vector3Int(x, y + 1, 0);
                Debug.Log("x:0, y:1 移動");
                this.transform.position = nextPos;
                y++;
            }
        }*/
    }
}
