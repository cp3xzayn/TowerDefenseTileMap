using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Start,
    WaveStart,
    Preparation,
    Battle,
    Result,
    Finish,
    GameOver
}

public class GameManager : MonoBehaviour
{
    GameObject m_mapGene;
    /// <summary>兵器を格納する配列</summary>
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
    /// <summary>ゲームオーバー時に表示するオブジェクト</summary>
    [SerializeField] GameObject m_gameoverText;
    /// <summary> 所持しているコストのテキスト </summary>
    [SerializeField] GameObject m_costObject;
    /// <summary> Waveが始まるときに表示するテキスト </summary>
    [SerializeField] GameObject m_waveObject;
    Text m_waveText;
    /// <summary> Waveクリア時に表示するText </summary>
    [SerializeField] Text m_waveClearText;
    /// <summary> WaveTimeを表示するText </summary>
    [SerializeField] Text m_waveTimeText;

    /// <summary> 準備期間の時間 /// </summary>
    [SerializeField] float m_preparationTimeSet = 10f;
    float m_preparationTime;
    /// <summary> 準備時間を一度だけセットするための変数 </summary>
    bool isPreTimeSet = true;

    /// <summary> Waveの時間 </summary>
    [SerializeField] float m_waveTimeSet = 20f;
    float m_waveTime;
    bool isWaveTime = true;
    /// <summary>現在のWave </summary>
    public int m_nowWave = 1;
    /// <summary> 敵生成の配列のIndexを進めるか判定する </summary>
    bool isIndexPulse;
    /// <summary> 取得した配列の長さ </summary>
    int m_eneGeneIndex;
    /// <summary> 敵生成時に用いるインデックス </summary>
    int m_index = 0;
    
    /// <summary> Waveが始まるときに表示するテキストの時間 </summary>
    float m_waveTextTime = 2f;
    float m_timer;
    /// <summary> m_timerを一度だけセットするための変数 </summary>
    bool isWaveTimeReset = true;

    /// <summary>敵生成の間隔 </summary>
    float m_eneGeneTime;
    float m_eTime = 0;
    /// <summary> 取得した敵の生成間隔 </summary>
    float m_eneGeneCoolTime;
    int m_eGCTIndex = 0;

    AudioSource audioSource;
    [SerializeField] AudioClip m_waveClearSound;
    [SerializeField] AudioClip m_gameOverSound;


    public static GameManager Instance;
    /// <summary>現在の状態 </summary>
    private GameState nowState;

    
    public float LoadEneGeneCoolTime(int index)
    {
        m_eGCTIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/EnemyGenerator").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
        m_eneGeneCoolTime = inputJson.m_waveCoolTime[m_eGCTIndex];
        return m_eneGeneCoolTime;
    }


    void Awake()
    {
        Instance = this;
        SetNowState(GameState.Start);
    }

    void Start()
    {
        m_preTimeText = m_preTimeObject.GetComponent<Text>();
        m_waveText = m_waveObject.GetComponent<Text>();
        m_eneGene = GameObject.Find("EnemyGenerator");
        EnemyGenerator e = m_eneGene.GetComponent<EnemyGenerator>();
        m_eneGeneIndex = e.GetLengthWave();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //WeaponManagerを探して配列に格納する
        m_wepMana = FindObjectsOfType<WeaponManager>();
        switch (nowState)
        {
            case GameState.WaveStart:
                WaveStartUpdate();
                break;
            case GameState.Preparation:
                PreparationUpdate();
                break;
            case GameState.Battle:
                BattleUpdate();
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
            case GameState.WaveStart:
                Debug.Log("GameState.WaveStart");
                break;
            case GameState.Preparation:
                Debug.Log("GameState.Preparation");
                break;
            case GameState.Battle:
                Debug.Log("GameState.Battle");
                break;
            case GameState.Result:
                Debug.Log("GameState.Result");
                ResultAction();
                break;
            case GameState.Finish:
                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                GameOverAction();
                break;
        }
    }

    /// <summary>
    /// GameState.Startに一回だけ呼ばれる処理
    /// </summary>
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
        m_costObject.SetActive(true);
        //GameStateを準備期間に変更する
        SetNowState(GameState.WaveStart);
    }

    /// <summary>
    /// GameState.WaveStartになったときに一度だけ呼ばれる関数
    /// </summary>
    void WaveStartUpdate()
    {
        if (isWaveTimeReset)
        {
            m_timer = 0;
            isWaveTimeReset = false;
        }
        m_waveObject.SetActive(true);
        m_waveText.text = "Wave " + m_nowWave;
        m_timer += Time.deltaTime;
        if (m_timer > m_waveTextTime)
        {
            m_waveObject.SetActive(false);
            if (m_nowWave == 1)
            {
                SetNowState(GameState.Preparation);
            }
            else
            {
                SetNowState(GameState.Battle);
            }
        }
    }

    //GameStateがPreparationになったときの処理
    void PreparationUpdate()
    {
        //準備時間を初期化する
        if (isPreTimeSet)
        {
            m_preparationTime = m_preparationTimeSet;
            isPreTimeSet = false;
        }
        m_preparationTime -= Time.deltaTime;
        m_preTimeText.text = "じゅんびじかん : " + m_preparationTime.ToString("f1");
        if (m_preparationTime < 0)
        {
            m_preTimeText.text = "じゅんびじかん : 0.0";
            //Battleに変更する
            SetNowState(GameState.Battle);
        }
    }

    /// <summary>
    /// GameStateがBattleになったときの処理
    /// </summary>
    void BattleUpdate()
    {
        m_eneGeneTime = LoadEneGeneCoolTime(m_eGCTIndex);
        EnemyGenerator e = m_eneGene.GetComponent<EnemyGenerator>();
        //生成のクールタイムが終わったら
        m_eTime += Time.deltaTime;
        if (m_eTime > m_eneGeneTime)
        {
            //敵を生成
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    e.EneGene(i, j, m_index);
                }
            }
            Debug.Log("敵生成");
            //m_indexを一度だけ１進める
            isIndexPulse = true;
            if (isIndexPulse)
            {
                m_index++;
                isIndexPulse = false;
            }
            m_eTime = 0;
        }
        //弾生成
        foreach (var item in m_wepMana)
        {
            item.OnShot();
        }

        //Wave時間を初期化する
        if (isWaveTime)
        {
            m_waveTime = m_waveTimeSet;
            isWaveTime = false;
        }
        m_waveTime -= Time.deltaTime;
        m_waveTimeText.text = "Waveじかん : " + m_waveTime.ToString("f1");
        if (m_waveTime < 0)
        {
            SetNowState(GameState.Result);
        }
    }

    //GameState.Resultになったときに一回だけ呼ばれる処理
    void ResultAction()
    {
        //時間を止める
        Time.timeScale = 0f;
        m_resultObject.SetActive(true);
        m_waveClearText.text = "Wave" + m_nowWave + "クリア";
        audioSource.PlayOneShot(m_waveClearSound);
    }

    //次へボタンが押されたときの処理
    public void OnClickNextWave()
    {
        m_nowWave++;
        m_eGCTIndex++;
        m_resultObject.SetActive(false);
        //PreparationTimeをセットするため
        isPreTimeSet = true;
        isWaveTimeReset = true;
        isWaveTime = true;
        //時間の動きを再開する
        Time.timeScale = 1f;
        Debug.Log("Wave" + m_nowWave);
        SetNowState(GameState.WaveStart);
    }

    //GameState.GameOverになったときに一回だけ呼ばれる処理
    void GameOverAction()
    {
        //Resultを表示
        m_gameoverText.SetActive(true);
        Time.timeScale = 0f;
        audioSource.PlayOneShot(m_gameOverSound);
    }

    /// <summary>
    /// playerを生成する関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void PlayerInstance(int x, int y)
    {
        Vector3Int plaPosition;
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
