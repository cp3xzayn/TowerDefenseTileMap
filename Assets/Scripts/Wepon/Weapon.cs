using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    GameObject m_weapon;
    GameObject m_weapon1;

    [SerializeField] AudioClip m_weaponClick;
    AudioSource audioSource;

    bool isWeapon = true;
    bool isWeapon1 = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Weaponのボタンが押されたとき
    public void OnClickWep()
    {
        audioSource.PlayOneShot(m_weaponClick);
        isWeapon = true;
        isWeapon1 = false;
    }
    //Weapon1のボタンが押されたとき
    public void OnClickWep1()
    {
        audioSource.PlayOneShot(m_weaponClick);
        isWeapon = false;
        isWeapon1 = true;
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
