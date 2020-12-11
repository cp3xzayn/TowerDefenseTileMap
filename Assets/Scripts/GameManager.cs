using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Start,
    Preparation,
    Battle,
    Result,
    Finish,
    GameOver
}

public class GameManager : MonoBehaviour
{
    GameObject m_mapGene;
    /// <summary>兵器</summary>
    WeaponManager[] m_wepMana;
    /// <summary>敵生成のオブジェクト</summary>
    GameObject m_eneGene;
    /// <summary>Playerのオブジェクト</summary>
    [SerializeField] GameObject m_player;
    /// <summary>準備時間のTextオブジェクト</summary>
    [SerializeField] GameObject m_preTimeObject;
    /// <summary>準備時間を表示するテキスト</summary>
    Text m_preTimeText;
    /// <summary>プレイヤーの生成ポジション </summary>
    Vector3Int plaPosition;

    /// <summary>準備期間の時間</summary>
    [SerializeField] float m_preparationTime = 10f;
    /// <summary>敵生成の間隔 </summary>
    [SerializeField] float m_eneTime = 3.0f;
    /// <summary>敵の生成上限 </summary>
    [SerializeField] int m_eneWave = 3;
    /// <summary>弾生成の間隔</summary>
    [SerializeField] float m_shootTime = 2.0f;
    public static GameManager Instance;
    //現在の状態
    private GameState nowState;


    void Awake()
    {
        Instance = this;
        SetNowState(GameState.Start);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_preTimeText = m_preTimeObject.GetComponent<Text>();
        m_eneGene = GameObject.Find("EnemyGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyGenerator e = m_eneGene.GetComponent<EnemyGenerator>();
        //WeaponManagerを探して配列に格納する
        m_wepMana = FindObjectsOfType<WeaponManager>();

        //GameStateがPreparationの時
        if (nowState == GameState.Preparation)
        {
            //準備時間が終わったら
            m_preparationTime -= Time.deltaTime;
            m_preTimeText.text = "制限時間 : " + m_preparationTime.ToString("f1");
            if (m_preparationTime < 0)
            {
                m_preTimeText.text = "制限時間 : 0.0";
                //Battleに変更する
                PreparationAction();
            }
        }
        //GameStateがBattleの時
        if (nowState == GameState.Battle)
        {
            //生成のクールタイムが終わったら
            m_eneTime -= Time.deltaTime;
            if (m_eneTime < 0)
            {
                if (m_eneWave > 0)
                {
                    //敵を生成
                    e.OnEneGene();
                }
                m_eneWave -= 1;
                m_eneTime = 10.0f;
            }
            //弾のクールタイムが終わったら
            m_shootTime -= Time.deltaTime;
            if (m_shootTime < 0)
            {
                foreach (var item in m_wepMana)
                {
                    //弾生成
                    item.OnShot();
                    m_shootTime = 2.0f;
                }
            }
            //敵の生成が終わったら
            if (m_eneWave == -1)
            {
                SetNowState(GameState.Result);
            }
        }
    }

    //外からこのメソッドを使って状態を変更
    public void SetNowState(GameState state)
    {
        nowState = state;
        OnStateChange(nowState);
    }

    //状態が変わったら何をするか
    void OnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Debug.Log("GameState.Start");
                StartAction();
                break;
            case GameState.Preparation:
                Debug.Log("GameState.Preparation");
                break;
            case GameState.Battle:
                Debug.Log("GameState.Battle");
                BattleAction();
                break;
            case GameState.Result:
                Debug.Log("GameState.Result");
                break;
            case GameState.Finish:
                break;
            case GameState.GameOver:
                break;
        }
    }
    
    public void OnClick()
    {
        SetNowState(GameState.Battle);
    }

    //GameStateがStartになったときの処理
    void StartAction()
    {
        m_mapGene = GameObject.Find("MapGenerator");
        //playerを生成する
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                PlayerInstance(i, j);
            }
        }
        //GameStateを準備期間に変更する
        SetNowState(GameState.Preparation);
    }

    //GameStateがPreparationになったときの処理
    void PreparationAction()
    {
        SetNowState(GameState.Battle);
    }

    //GameStateがBattleになったときの処理
    void BattleAction()
    {
        
    }
    //GameStateがResultになったときの処理
    void ResultAction()
    {

    }


    //playerを生成する関数
    void PlayerInstance(int x, int y)
    {
        //Mapの情報を取得する
        MapGenerator t = m_mapGene.GetComponent<MapGenerator>();
        int[,] m = t.Map;
        plaPosition = new Vector3Int(x, y, 0);
        if (m[x, y] == 6)
        {
            Instantiate(m_player, plaPosition, Quaternion.identity);
        }
    }
}
