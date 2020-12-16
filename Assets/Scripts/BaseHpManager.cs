using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHpManager : MonoBehaviour
{
    /// <summary> 拠点の耐久値 </summary>
    [SerializeField] int m_baseHP = 20;
    /// <summary>敵 </summary>
    EnemyController[] m_enemy;
    /// <summary> 敵の攻撃力 </summary>
    int m_eAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WeaponManagerを探して配列に格納する
        m_enemy = FindObjectsOfType<EnemyController>();
        foreach (var item in m_enemy)
        {
        }
    }

    void DecreaseHP()
    {
        
    }
}
