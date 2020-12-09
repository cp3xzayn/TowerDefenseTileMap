﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //GameObject m_wepMana;
    WeaponManager[] m_wepMana;
    //WeaponManager[] m_wepMana;
    [SerializeField] GameObject m_player;
    /// <summary>プレイヤーの生成ポジション </summary>
    Vector3Int plaPosition;

    bool isShot;
    //準備期間の時間
    [SerializeField] float m_preparationTime = 5f;
    //弾生成の間隔
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                //時間が経ったらBattleに変更する
                break;
            case GameState.Battle:
                Debug.Log("GameState.Battle");
                BattleAction();
                break;
            case GameState.Result:
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
        //WeaponManagerを探して配列に格納する
        m_wepMana = FindObjectsOfType<WeaponManager>();
        foreach (var item in m_wepMana)
        {
            //弾生成の関数
            item.OnShot();
        }
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
