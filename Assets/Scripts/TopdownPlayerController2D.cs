using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class TopdownPlayerController2D : MonoBehaviour
{
    GameObject m_mapGene;
    GameObject m_wepMana;
    /// <summary>移動速度</summary>
    [SerializeField] float m_walkSpeed = 1f;
    /// <summary>直前に移動した方向</summary>
    Vector2 m_lastMovedDirection;
    SpriteRenderer m_sprite;
    Animator m_anim;
    Rigidbody2D m_rb;
    bool m_isWalking;
    /// <summary> プレイヤーのポジション </summary>
    Vector3 plaPos;
    /// <summary>兵器を置くポジション</summary>
    Vector3Int weaPos;
    /// <summary>置くことができる兵器の制限 </summary>
    int m_cost;

    /// <summary>兵器のコスト </summary>
    int m_weaponCost = 1;

    GameObject m_costMana;

    Weapon weapon;

    Button button;
    GameObject m_resetButton;

    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_mapGene = GameObject.Find("MapGenerator");
        m_wepMana = GameObject.Find("WeaponManager");
        m_costMana = GameObject.Find("CostManager");
        weapon = m_wepMana.GetComponent<Weapon>();

        //Playerのポジションをリセットする処理を書く。ボタンを設定する
        /*m_resetButton = GameObject.Find("PlayerPosReset");
        button = m_resetButton.GetComponent<Button>();
        button.onClick.AddListener(OnClickPosReset);*/
    }

    void Update()
    {
        //コストを取得する
        CostManager c = m_costMana.GetComponent<CostManager>();
        m_cost = c.Cost;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        plaPos = this.transform.position;
        Vector2 dir = AdjustInputDirection(h, v);   // 入力方向を４方向に変換（制限）する
        // オブジェクトを動かす
        //transform.Translate(m_walkSpeed * dir * Time.deltaTime);    // このやり方でもできるが、コライダーにめり込む
        m_rb.velocity = dir * m_walkSpeed;
        // 入力方向によって左右の向きを変える
        if (dir.x != 0)
        {
            m_sprite.flipX = (dir.x > 0);
        }

        //Animate(dir.x, dir.y);
        if (dir == Vector2.zero)
        {
            m_isWalking = false;
        }
        else
        {
            m_isWalking = true;
        }
        m_anim.SetBool("IsWalking", m_isWalking);
        m_anim.SetFloat("InputX", Mathf.Abs(dir.x));
        m_anim.SetFloat("InputY", dir.y);
        m_lastMovedDirection = dir;

        //Mapの情報を取得する
        MapGenerator t = m_mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        //playerのポジションを四捨五入して兵器のポジションを決める
        weaPos.x = Mathf.RoundToInt(plaPos.x);
        weaPos.y = Mathf.RoundToInt(plaPos.y);
        //拠点以外のタイルの場合に
        if (m[weaPos.x, weaPos.y] != 5 && m[weaPos.x, weaPos.y] != 6)
        {
            //兵器コストがあるときに
            if (m_cost > 0)
            {
                //SpaceKeyを押されたとき兵器を置く
                if (Input.GetKeyDown("space"))
                {
                    weapon.WeaponInstance(weaPos);
                    StartCoroutine("SetWeapon");
                    //コストを減らす
                    m_cost -= m_weaponCost;
                    //CostManager c = m_costMana.GetComponent<CostManager>();
                    c.DecreaseCost();
                }
            }
        }
    }
    //兵器を置いたときにLayerを変更してぶつかることを阻止する
    IEnumerator SetWeapon()
    {
        //レイヤーをWeaponSetに変更
        gameObject.layer = LayerMask.NameToLayer("WeaponSet");
        yield return new WaitForSeconds(1f);
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// 入力された方向を４方向に制限し、Vector2 にして返す
    /// （斜めに入力された場合でも、それ以前の入力状況に応じて４方向に制限する）
    /// </summary>
    /// <param name="inputX"></param>
    /// <param name="inputY"></param>
    Vector2 AdjustInputDirection(float inputX, float inputY)
    {
        Vector2 dir = new Vector2(inputX, inputY);

        if (m_lastMovedDirection == Vector2.zero)
        {
            if (dir.x != 0 && dir.y != 0)
            {
                dir.y = 0;
            }
        }
        else if (m_lastMovedDirection.x != 0)
        {
            dir.y = 0;
        }
        else if (m_lastMovedDirection.y != 0)
        {
            dir.x = 0f;
        }

        return dir;
    }

    /// <summary>
    /// 入力と直前に移動した方向に応じてアニメーションを制御する
    /// </summary>
    /// <param name="inputX"></param>
    /// <param name="inputY"></param>
    void Animate(float inputX, float inputY)
    {
        if (m_anim == null) return; // Animator Controller がない場合は何もしない

        if (inputX != 0)
        {
            m_anim.Play("WalkLeft");
        }
        else if (inputY > 0)
        {
            m_anim.Play("WalkUp");
        }
        else if (inputY < 0)
        {
            m_anim.Play("WalkDown");
        }
        else
        {
            if (m_lastMovedDirection.x != 0)
            {
                m_anim.Play("IdolLeft");
            }
            else if (m_lastMovedDirection.y > 0)
            {
                m_anim.Play("IdolUp");
            }
            else if (m_lastMovedDirection.y < 0)
            {
                m_anim.Play("IdolDown");
            }
        }
    }

    void PlayerPosReset(int x, int y)
    {
        Vector3Int plaPosition;
        //Mapの情報を取得する
        MapGenerator t = m_mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        plaPosition = new Vector3Int(x, y, 0);
        if (m[x, y] == 6)
        {
            this.transform.position = plaPosition;
        }
    }

    public void OnClickPosReset()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                PlayerPosReset(i, j);
            }
        }
    }
}
