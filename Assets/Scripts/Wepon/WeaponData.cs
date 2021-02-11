using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class InputJsonWeaponData
{
    public float[] m_weaponShootData;
}

public class WeaponData : MonoBehaviour
{
    /// <summary> 配列の要素数 </summary>
    int m_index = 0;
    /// <summary> 発射間隔 </summary>
    float m_shootingTime;
    /// <summary> 強化ボタンのクールタイム </summary>
    float m_strCoolTime = 5.0f;
    float m_time;

    int m_needCost = 10;

    Button m_strWeaponButton;
    GameObject m_weaponStr;

    WeaponManager wMana;

    bool isStr = true;

    /// <summary>
    /// Jsonファイルから兵器の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadWeaponData(int index)
    {
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_shootingTime = inputJsonWeaponData.m_weaponShootData[m_index];
        return m_shootingTime;
    }

    private void Start()
    {
        // 発射間隔を初期化する
        m_shootingTime = LoadWeaponData(m_index);
        //Debug.Log("Weapon ShootTime:" + m_shootingTime);
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootingTime);
    }
}
