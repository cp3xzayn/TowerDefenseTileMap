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
        Debug.Log("Weapon ShootTime:" + m_shootingTime);
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootingTime);
    }

    void Update()
    {
        //WeapnStrButtonのOnClickを設定する
        m_weaponStr = GameObject.Find("WeaponStr");
        if (m_weaponStr != null)
        {
            m_strWeaponButton = m_weaponStr.GetComponent<Button>();
            m_strWeaponButton.onClick.AddListener(OnClickWeaponStren);
        }

        if (!isStr)
        {
            //クールタイムが終わったらまた強化できるようにする
            m_time += Time.deltaTime;
            if (m_time > m_strCoolTime)
            {
                isStr = true;
            }
        }
        Debug.Log("Weapon ShootTime:" + m_shootingTime);
    }

    // 兵器の強化ボタンが押されたとき
    public void OnClickWeaponStren()
    {
        // indexを一つ進める
        if (isStr)
        {
            m_index++;
            isStr = false;
            m_shootingTime = LoadWeaponData(m_index);
            wMana = GetComponent<WeaponManager>();
            wMana.SetWeaponData(m_shootingTime);
        }
    }
}
