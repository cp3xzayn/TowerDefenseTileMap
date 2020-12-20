using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] float m_shootTime = 2f;
    WeaponManager wMana;

    void Start()
    {
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootTime);
    }
}
