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
    /// <summary>ResultのTextオブジェクト </summary>
    [SerializeField] GameObject m_resultObject;
    /// <summary>Resultを表示するText </summary>
    Text m_resultText;
    [SerializeField] GameObject m_resultPanel;
    /// <summary>プレイヤーの生成ポジション </summary>
    Vector3Int plaPosition;
    /// <summary>プレイヤーのポジションを生成ポジに戻すための関数 </summary>
    Vector3Int plaRestPosition;

    /// <summary>準備期間の時間</summary>
    [SerializeField] float m_preparationTime = 10f;
    /// <summary>敵生成の間隔 </summary>
    [SerializeField] float m_eneGeneTime = 3.0f;
    /// <summary>敵の生成上限 </summary>
    [SerializeField] int m_eneWave = 3;
    /// <summary>弾生成の間隔</summary>
    [SerializeField] float m_shootTime = 2.0f;
    //獲得コスト
    int m_getCost = 3;

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
        m_resultText = m_resultObject.GetComponent<Text>();
        m_eneGene = GameObject.Find("EnemyGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        //WeaponManagerを探して配列に格納する
        m_wepMana = FindObjectsOfType<WeaponManager>();
        switch (nowState)
        {
            case GameState.Preparation:
                PreparationUpdate();
                break;
            case GameState.Battle:
                BattleUpdate();
                break;
            case GameState.Result:
                break;
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
                break;
            case GameState.Result:
                Debug.Log("GameState.Result");
                //ResultAction();
                break;
            case GameState.Finish:
                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                break;
        }
    }
    
    //GameState.Startに一回だけ呼ばれる処理
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
    void PreparationUpdate()
    {
        //準備時間が終わったら
        m_preparationTime -= Time.deltaTime;
        m_preTimeText.text = "準備時間 : " + m_preparationTime.ToString("f1");
        if (m_preparationTime < 0)
        {
            m_preTimeText.text = "準備時間 : 0.0";
            //Battleに変更する
            SetNowState(GameState.Battle);
        }
    }

    //GameStateがBattleになったときの処理
    void BattleUpdate()
    {
        EnemyGenerator e = m_eneGene.GetComponent<EnemyGenerator>();
        //生成のクールタイムが終わったら
        m_eneGeneTime -= Time.deltaTime;
        if (m_eneGeneTime < 0)
        {
            if (m_eneWave >= 1)
            {
                //敵を生成
                e.OnEneGene();
                Debug.Log("敵生成");
            }
            else if (m_eneWave == 0)
            {
                //Bossを生成
                e.OnBossGene();
                Debug.Log("Boss生成");
            }
            m_eneWave -= 1;
            m_eneGeneTime = 5.0f;
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
        if (m_eneWave == -2)
        {
            //Resultに変更する
            SetNowState(GameState.Result);
        }
    }

    //GameState.Resultになったときに一回だけ呼ばれる処理
    void ResultAction()
    {
        m_resultPanel.SetActive(true);
        m_resultText.text = "獲得兵器コスト:" + m_getCost;
    }
    //GameState.GameOverになったときに一回だけ呼ばれる処理
    void GameOverAction()
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
