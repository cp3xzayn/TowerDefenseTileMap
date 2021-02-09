using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] float m_shootTime = 2f;

    public float ShootTime { get { return m_shootTime; } }

    WeaponManager wMana;

    void Start()
    {
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootTime);
    }
}
