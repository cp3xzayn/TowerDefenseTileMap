using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    GameObject m_weapon;
    GameObject m_weapon1;

    bool isWeapon = false;
    bool isWeapon1 = false;

    //Weaponのボタンが押されたとき
    public void OnClickWep()
    {
        isWeapon = true;
        isWeapon1 = false;
        Debug.Log(isWeapon1);
    }
    //Weapon1のボタンが押されたとき
    public void OnClickWep1()
    {
        isWeapon = false;
        isWeapon1 = true;
        Debug.Log(isWeapon1);
    }

    /// <summary>
    /// 兵器を生成する関数
    /// </summary>
    /// <param name="wepPos"></param>
    public void WeaponInstance(Vector3Int wepPos)
    {
        if (isWeapon)
        {
            m_weapon = Resources.Load<GameObject>("Weapon");
            Instantiate(m_weapon, wepPos, Quaternion.identity);
        }
        if (isWeapon1)
        {
            m_weapon1 = Resources.Load<GameObject>("Weapon1");
            Instantiate(m_weapon1, wepPos, Quaternion.identity);
        }
    }
}
