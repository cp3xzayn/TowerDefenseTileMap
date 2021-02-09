using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputJsonWeaponData
{
    public float[] m_weaponData;
}

public class WeaponData : MonoBehaviour
{
    [SerializeField] float m_shootTime = 2f;

    int m_index;
    float m_shootingTime;

    public float LoadWeaponData(int index)
    {
        m_index = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_shootingTime = inputJsonWeaponData.m_weaponData[m_index];
        return m_shootingTime;
    }


    public float ShootTime { get { return m_shootTime; } }

    WeaponManager wMana;

    void Start()
    {
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootTime);
    }

    void OnClickWeaponStren()
    {

    }
}
