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
    /// <summary>敵を格納する配列 </summary>
    EnemyController[] m_enemy;
    /// <summary>敵生成のオブジェクト</summary>
    GameObject m_eneGene;
    /// <summary>Playerのオブジェクト</summary>
    [SerializeField] GameObject m_player;
    /// <summary> Canvasのオブジェクト </summary>
    [SerializeField] GameObject m_canvas;
    /// <summary>準備時間のTextオブジェクト</summary>
    [SerializeField] GameObject m_preTimeObject;
    /// <summary>準備時間を表示するテキスト</summary>
    Text m_preTimeText;
    /// <summary>ResultのTextオブジェクト </summary>
    [SerializeField] GameObject m_resultObject;
    /// <summary>ゲームオーバー時に表示するオブジェクト</summary>
    [SerializeField] GameObject m_gameoverText;
    /// <summary>プレイヤーの生成ポジション </summary>
    Vector3Int plaPosition;
    /// <summary>プレイヤーのポジションを生成ポジに戻すための関数 </summary>
    Vector3Int plaRestPosition;
    /// <summary>拠点の耐久値のSlider </summary>
    [SerializeField]GameObject m_baseHPSlider;
    /// <summary>兵器選択ボタン</summary>
    [SerializeField] GameObject m_weapon;
    /// <summary>兵器1選択ボタン </summary>
    [SerializeField] GameObject m_weapon1;
    /// <summary>背景のTileSet</summary>
    [SerializeField] GameObject m_backGroundTileSet;
    /// <summary> 所持しているコストのテキスト </summary>
    [SerializeField] GameObject m_costObject;
    /// <summary> Waveが始まるときに表示するテキスト </summary>
    [SerializeField] GameObject m_waveObject;
    Text m_waveText;

    /// <summary> 準備期間の時間 /// </summary>
    [SerializeField] float m_preparationTimeSet = 10f;
    float m_preparationTime;
    /// <summary>敵生成の間隔 </summary>
    [SerializeField] float m_eneGeneTime = 3.0f;
    float m_eTime = 0;
    /// <summary>敵の生成上限 </summary>
    [SerializeField] int m_eneWave = 3;
    /// <summary> 敵生成の配列のIndexを進めるか判定する </summary>
    bool isIndexPulse;
    /// <summary> 取得した配列の長さ </summary>
    int m_eneGeneIndex1;
    int m_eneGeneIndex2;
    /// <summary> 敵生成時に用いるインデックス </summary>
    int m_index = 0;
    /// <summary>現在のWave </summary>
    public int m_nowWave = 1;
    /// <summary> 準備時間を一度だけセットするための変数 </summary>
    bool isPreTimeSet = true;

    /// <summary> Waveが始まるときに表示するテキストの時間 </summary>
    float m_waveTextTime = 2f;
    float m_timer;
    /// <summary> m_timerを一度だけセットするための変数 </summary>
    bool isWaveTimeReset = true;

    //仮にタイマーをセットしている
    float m_nextTime = 7;
    float m_time = 0;

    public static GameManager Instance;
    /// <summary>現在の状態 </summary>
    private GameState nowState;
 
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
        m_eneGeneIndex1 = e.GetLengthWave1();
        m_eneGeneIndex2 = e.GetLengthWave2();
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
        m_baseHPSlider.SetActive(true);
        m_weapon.SetActive(true);
        m_weapon1.SetActive(true);
        m_backGroundTileSet.SetActive(true);
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
            SetNowState(GameState.Preparation);
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
        //現在のWaveに応じて敵の生成を変える
        switch (m_nowWave)
        {
            case 1:
                //生成のクールタイムが終わったら
                m_eTime += Time.deltaTime;
                if (m_eTime > m_eneGeneTime)
                {
                    //敵生成の配列の長さ分だけループさせる
                    if (m_eneGeneIndex1 > 0)
                    {
                        //敵を生成
                        for (int i = 0; i < 13; i++)
                        {
                            for (int j = 0; j < 13; j++)
                            {
                                e.EneGene1(i, j, m_index);
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
                    }
                    m_eneGeneIndex1--;
                    m_eTime = 0;
                }
                if (m_eneGeneIndex1 <= 0)
                {
                    /*m_enemy = FindObjectsOfType<EnemyController>();
                    if (m_enemy == null)
                    {
                        Debug.Log("Wave終了");
                        SetNowState(GameState.Result);
                    }*/
                    m_time += Time.deltaTime;
                    if (m_time > m_nextTime)
                    {
                        //indexを初期化する
                        m_index = 0;
                        SetNowState(GameState.Result);
                    }
                }
                break;
            case 2:
                Debug.Log("Wave2");
                //生成のクールタイムが終わったら
                m_eTime += Time.deltaTime;
                if (m_eTime > m_eneGeneTime)
                {
                    //敵生成の配列の長さ分だけループさせる
                    if (m_eneGeneIndex2 > 0)
                    {
                        //敵を生成
                        for (int i = 0; i < 13; i++)
                        {
                            for (int j = 0; j < 13; j++)
                            {
                                e.EneGene2(i, j, m_index);
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
                    }
                    m_eneGeneIndex2--;
                    m_eTime = 0;
                }
                if (m_eneGeneIndex2<= 0)
                {
                    /*m_enemy = FindObjectsOfType<EnemyController>();
                    if (m_enemy == null)
                    {
                        Debug.Log("Wave終了");
                        SetNowState(GameState.Result);
                    }*/
                    m_time += Time.deltaTime;
                    if (m_time > m_nextTime)
                    {
                        //indexを初期化する
                        m_index = 0;
                        SetNowState(GameState.Result);
                    }
                }
                break;
        }
        //弾生成
        foreach (var item in m_wepMana)
        {
            item.OnShot();
        }
    }

    //GameState.Resultになったときに一回だけ呼ばれる処理
    void ResultAction()
    {
        //時間を止める
        Time.timeScale = 0f;
        //ResultPanelのprefabを生成する
        //GameObject resultPrefab = (GameObject)Instantiate(m_resultObject);
        //resultPrefab.transform.SetParent(m_canvas.transform, false);
        m_resultObject.SetActive(true);
    }
    //次へボタンが押されたときの処理
    public void OnClickNextWave()
    {
        m_nowWave++;
        m_resultObject.SetActive(false);
        //PreparationTimeをセットするため
        isPreTimeSet = true;
        isWaveTimeReset = true;
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
    }

    /// <summary>
    /// playerを生成する関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
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



    /// <summary>
    /// Playerのポジションを拠点に戻すための変数
    /// </summary>
    public void PlayerPosReset()
    {
        //まだ未実装
        //Debug.Log("PlyaerPosReset");
        //m_player.transform.position = new Vector3Int(6, 6, 0);
    }
}
