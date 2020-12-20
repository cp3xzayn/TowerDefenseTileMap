using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Data : MonoBehaviour
{
    [SerializeField] float m_shootTime = 4f;
    WeaponManager wMana;

    void Start()
    {
        wMana = GetComponent<WeaponManager>();
        wMana.SetWeaponData(m_shootTime);
    }

}
