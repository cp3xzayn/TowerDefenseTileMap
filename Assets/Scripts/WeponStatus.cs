using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputJsonWeaponData
{
    public float[] m_weaponShootData;
}

public class WeponStatus : MonoBehaviour
{
    /// <summary> 配列の要素数 </summary>
    int m_index = 0;
    /// <summary> 発射間隔 </summary>
    float m_shootingTime;

    WeaponManager wMana;

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

    // 兵器の強化ボタンが押されたとき
    public void OnClickWeaponStren()
    {
        // indexを一つ進める
        m_index++;
        m_shootingTime = LoadWeaponData(m_index);
        Debug.Log("a" + m_shootingTime);
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootingTime);
    }
}
