using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    /// <summary> 配列の要素数 </summary>
    int m_index = 0;
    /// <summary> 発射間隔 </summary>
    float m_shootingTime;

    WeaponManager wMana;
    WeponStatus wStas;

    GameObject m_weaponStatus;

    private void Start()
    {
        m_weaponStatus = GameObject.Find("WeaponStatusManager");
    }

    void Update()
    {
        // 発射間隔を初期化する
        wStas = m_weaponStatus.GetComponent<WeponStatus>();
        m_shootingTime = wStas.LoadWeaponData(m_index);
        Debug.Log("Weapon ShootTime:" + m_shootingTime);
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootingTime);
    }
}
