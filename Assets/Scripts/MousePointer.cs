using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    //表示するUI
    [SerializeField] GameObject m_weaponStatus;
    [SerializeField] GameObject m_weapon1Status;
    //表示するUIのテキスト
    [SerializeField] Text m_statusText;
    [SerializeField] Text m_status1Text;


    int wDamage = 1;
    float wRange = 5;
    float wShootTime = 2;
    int w1Damage = 3;
    float w1Range = 5;
    float w1ShootTime = 4;


    void Update()
    {
        //SetWepStatusText();
    }

    /// <summary>
    /// WeaponのステータスのTextを表示する関数
    /// </summary>
    void SetWepStatusText()
    {
        m_statusText.text = "威力:" + wDamage + "\n" + "射程:" + wRange + "\n" + "レート:" + wShootTime;
        m_status1Text.text = "威力:" + w1Damage + "\n" + "射程:" + w1Range + "\n" + "レート:" + w1ShootTime;
    }
}
