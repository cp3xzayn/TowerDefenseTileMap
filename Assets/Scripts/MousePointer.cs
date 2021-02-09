using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    /// <summary> MouseカーソルのＸ座標 </summary>
    float m_mouseX;
    /// <summary> MouseカーソルのＸ座標 </summary>
    float m_mouseY;
    
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
        WeaponStatusVisible();
        SetWepStatusText();
    }

    /// <summary>
    /// WeaponのステータスのTextを表示する関数
    /// </summary>
    void SetWepStatusText()
    {
        m_statusText.text = "威力:" + wDamage + "\n" + "射程:" + wRange + "\n" + "レート:" + wShootTime;
        m_status1Text.text = "威力:" + w1Damage + "\n" + "射程:" + w1Range + "\n" + "レート:" + w1ShootTime;
    }


    void WeaponStatusVisible()
    {
        m_mouseX = Input.mousePosition.x;
        m_mouseY = Input.mousePosition.y;
        //マウスカーソルが範囲内にある時にUIを表示する
        if (m_mouseX > 30 && m_mouseX < 65 && m_mouseY > 360 && m_mouseY < 405)
        {
            m_weaponStatus.SetActive(true);
        }
        else
        {
            m_weaponStatus.SetActive(false);
        }

        if (m_mouseX > 30 && m_mouseX < 65 && m_mouseY > 232 && m_mouseY < 270)
        {
            m_weapon1Status.SetActive(true);
        }
        else
        {
            m_weapon1Status.SetActive(false);
        }
    }


}
